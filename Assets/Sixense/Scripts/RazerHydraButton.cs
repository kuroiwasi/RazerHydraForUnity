using static Sixense.SixenseLib;

namespace Sixense
{
    public class RazerHydraButton
    {
        public enum TYPE
        {
            START = SIXENSE_BUTTON.START,
            BUTTON_1 = SIXENSE_BUTTON.BUTTON_1,
            BUTTON_2 = SIXENSE_BUTTON.BUTTON_2,
            BUTTON_3 = SIXENSE_BUTTON.BUTTON_3,
            BUTTON_4 = SIXENSE_BUTTON.BUTTON_4,
            BUMPER = SIXENSE_BUTTON.BUMPER,
            JOYSTICK = SIXENSE_BUTTON.JOYSTICK
        }

        private enum STATE
        {
            RELEASED,
            PRESSED
        }

        public TYPE Type { get; private set; }
        private STATE prevState;
        private STATE currentState;

        public RazerHydraButton(TYPE type)
        {
            Type = type;
            prevState = STATE.RELEASED;
            currentState = STATE.RELEASED;
        }

        public void Update(SixenseControllerData data)
        {
            prevState = currentState;

            if ((data.buttons & (int)Type) > 0)
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