using System.Diagnostics;
using System.Numerics;
using static Tilengine.TLN;

namespace Barrel
{
    internal class Simon
    {
        private const float MoveSpeed = 50f;
        private readonly IntPtr _sequencePack;
        private readonly IntPtr _spriteSet;
        private readonly IntPtr _walkSequence;
        private Vector2 _previousVelocity;
        private Vector2 _velocity;
        private Vector2 _position;
        private Vector2 _displayPosition;
        private int _xworld;
        private State _state;
        private Direction _direction;

        private enum State
        {
            Idle,
            Walking,
            Airborne
        }

        private enum Direction
        {
            None,
            Left,
            Right
        }

        public Simon()
        {
            _spriteSet = TLN_LoadSpriteset("Simon");
            _sequencePack = TLN_LoadSequencePack("Simon.sqx");
            _walkSequence = TLN_FindSequence(_sequencePack, "walk");

            TLN_SetSpriteSet(0, _spriteSet);
            _direction = Direction.Right;
            SetState(State.Idle);
        }

        private void SetState(State s)
        {
            if (_state == s)
                return;

            _state = s;
            switch (_state)
            {
                case State.Idle:
                    TLN_DisableSpriteAnimation(0);
                    TLN_SetSpritePicture(0, 0);
                    break;

                case State.Walking:
                    TLN_SetSpriteAnimation(0, _walkSequence, 0);
                    break;

                case State.Airborne:
                    TLN_DisableSpriteAnimation(0);
                    TLN_SetSpritePicture(0, 7);
                    break;
            }
        }

        /// <summary>
        /// Performs all tasks to update the character.
        /// </summary>
        public void Tasks(float deltaTime)
        {
            // Warp the character to the top when he is falling past the
            // framebuffer height.
            if (_position.Y > TLN_GetHeight())
            {
                _velocity.Y /= 2;
                _position.Y = -48;
            }

            // Save and reset the velocity.
            _previousVelocity = _velocity;
            _velocity.X = 0;

            if (TLN_GetInput(TLN_Input.INPUT_LEFT))
            {
                _velocity.X = -MoveSpeed;

                if (_direction == Direction.Right)
                {
                    TLN_SetSpriteFlags(0, TLN_TileFlags.FLAG_FLIPX);
                }

                _direction = Direction.Left;
            }
            else if (TLN_GetInput(TLN_Input.INPUT_RIGHT))
            {
                _velocity.X = MoveSpeed;

                if (_direction == Direction.Left)
                {
                    TLN_SetSpriteFlags(0, 0);
                }

                _direction = Direction.Right;
            }

            if (IsGrounded())
            {
                _velocity.Y = 0;

                if (TLN_GetInput(TLN_Input.INPUT_BUTTON1))
                {
                    _velocity.Y = -MoveSpeed;
                }
                else if (TLN_GetInput(TLN_Input.INPUT_RIGHT) || TLN_GetInput(TLN_Input.INPUT_LEFT))
                {
                    SetState(State.Walking);
                }
                else
                {
                    SetState(State.Idle);
                }
            }
            else
            {
                _velocity.Y += (MoveSpeed * 2) * deltaTime;

                if (_state != State.Airborne)
                {
                    SetState(State.Airborne);
                }
            }

            _position += (_velocity + _previousVelocity) * deltaTime;
            TLN_SetSpritePosition(0, (int)_position.X, (int)_position.Y);
        }

        private bool IsGrounded()
        {
            for (var c = 8; c < 24; c += 8)
            {
                TLN_GetLayerTile(0, (int)_position.X + c + _xworld, (int)_position.Y + 48, out var ti);
                if (ti.index > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public int GetPositionX()
        {
            return _xworld;
        }

        public void Deinit()
        {
            TLN_DeleteSequencePack(_sequencePack);
            TLN_DeleteSpriteset(_spriteSet);
        }
    }
}