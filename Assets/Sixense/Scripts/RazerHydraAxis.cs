using UnityEngine;
using static Sixense.SixenseLib;

namespace Sixense
{
    public class RazerHydraAxis
    {
        public enum TYPE
        {
            JOYSTICK_X,
            JOYSTICK_Y,
            TRIGGER
        }

        private enum STATE
        {
            RELEASED,
            PRESSED
        }

        public TYPE Type { get; private set; }
        public float Value { get; private set; }
        private STATE prevState;
        private STATE currentState;
        private readonly float threshold;

        public RazerHydraAxis(TYPE type, float threshold)
        {
            Type = type;
            prevState = STATE.RELEASED;
            currentState = STATE.RELEASED;
            this.threshold = threshold;
        }

        public void Update(SixenseControllerData data)
        {
            prevState = currentState;

            switch (Type)
            {
                case TYPE.TRIGGER:
                    Value = data.trigger;
                    break;
                case TYPE.JOYSTICK_X:
                    Value = data.joystick_x;
                    break;
                case TYPE.JOYSTICK_Y:
                    Value = data.joystick_y;
                    break;
            }

            if (Mathf.Abs(Value) > threshold)
            {
                currentState = STATE.PRESSED;
            }
            else
            {
                currentState = STATE.RELEASED;
            }
        }

        public bool IsDown()
        {
            return currentState == STATE.PRESSED && prevState == STATE.RELEASED;
        }

        public bool IsPressed()
        {
            return currentState == STATE.PRESSED;
        }

        public bool IsUp()
        {
            return currentState == STATE.RELEASED && prevState == STATE.RELEASED;
        }
    }
}
