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
        private Direction _direction;
        private Vector2 _position;
        private Vector2 _previousVelocity;
        private State _state;
        private Vector2 _velocity;
        private int _xworld;

        public Simon()
        {
            // Load sprites and sequences.
            _spriteSet = TLN_LoadSpriteset("Simon");
            _sequencePack = TLN_LoadSequencePack("Simon.sqx");
            _walkSequence = TLN_FindSequence(_sequencePack, "walk");

            // Set initial state.
            TLN_SetSpriteSet(0, _spriteSet);
            _direction = Direction.Right;
            SetState(State.Idle);
        }

        private enum Direction
        {
            None,
            Left,
            Right
        }

        private enum State
        {
            Idle,
            Walking,
            Airborne
        }

        /// <summary>
        /// Disposes of the character's resources.
        /// </summary>
        public void Deinit()
        {
            TLN_DeleteSequencePack(_sequencePack);
            TLN_DeleteSpriteset(_spriteSet);
        }

        /// <summary>
        /// Gets the scroll position of the world.
        /// </summary>
        /// <returns>The scroll position.</returns>
        public int GetPositionX()
        {
            return _xworld;
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

            // Handle input.
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

            // Update the character's position.
            if (IsGrounded())
            {
                // Set the character to walking state if he is moving.
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
                // Set the character to airborne state if he is falling.
                _velocity.Y += (MoveSpeed * 2) * deltaTime;

                if (_state != State.Airborne)
                {
                    SetState(State.Airborne);
                }
            }

            _position += (_velocity + _previousVelocity) * deltaTime;

            // Make sure the character doesn't go off the screen.
            if (_position.X < 0)
            {
                _position.X = 0;
                _velocity.X = 0;
            }

            // Follow the character by setting the horizontal scroll position
            // after a certain threshold.
            if (_position.X <= 120)
            {
                TLN_SetSpritePosition(0, (int)_position.X, (int)_position.Y);
            }
            else
            {
                TLN_SetSpritePosition(0, 120, (int)_position.Y);
                _xworld = (int)_position.X - 120;
            }
        }

        /// <summary>
        /// Returns true if the character is on the ground.
        /// </summary>
        /// <returns>true if grounded, otherwise false.</returns>
        private bool IsGrounded()
        {
            for (var c = 8; c < 24; c += 8)
            {
                TLN_GetLayerTile(0, (int)_position.X + c, (int)_position.Y + 48, out var ti);
                if (ti.index > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Updates the character's sprite and animation.
        /// </summary>
        /// <param name="state">The state to change.</param>
        private void SetState(State state)
        {
            if (_state == state)
            {
                return;
            }

            _state = state;
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
    }
}