using System.Collections.Generic;
using UnityEngine;
using static Sixense.SixenseLib;

namespace Sixense
{
    public class RazerHydraInput : MonoBehaviour
    {
        public enum CONTROLLER_ID
        {
            ZERO = 0,
            ONE = 1
        }

        public CONTROLLER_ID ControllerID = CONTROLLER_ID.ZERO;
        public float JoystickPressThreshold = 0;
        public float TriggerPressThreshold = 0.5f;
        private SixenseControllerData currentData;
        private readonly float scaleFactor = 0.001f;
        private Dictionary<RazerHydraButton.TYPE, RazerHydraButton> buttons;
        private Dictionary<RazerHydraAxis.TYPE, RazerHydraAxis> axis;

        void Start()
        {
            buttons = new Dictionary<RazerHydraButton.TYPE, RazerHydraButton>
            {
                { RazerHydraButton.TYPE.BUMPER, new RazerHydraButton(RazerHydraButton.TYPE.BUMPER) },
                { RazerHydraButton.TYPE.BUTTON_1, new RazerHydraButton(RazerHydraButton.TYPE.BUTTON_1) },
                { RazerHydraButton.TYPE.BUTTON_2, new RazerHydraButton(RazerHydraButton.TYPE.BUTTON_2) },
                { RazerHydraButton.TYPE.BUTTON_3, new RazerHydraButton(RazerHydraButton.TYPE.BUTTON_3) },
                { RazerHydraButton.TYPE.BUTTON_4, new RazerHydraButton(RazerHydraButton.TYPE.BUTTON_4) },
                { RazerHydraButton.TYPE.JOYSTICK, new RazerHydraButton(RazerHydraButton.TYPE.JOYSTICK) },
                { RazerHydraButton.TYPE.START, new RazerHydraButton(RazerHydraButton.TYPE.START) }
            };

            axis = new Dictionary<RazerHydraAxis.TYPE, RazerHydraAxis>
            {
                { RazerHydraAxis.TYPE.JOYSTICK_X, new RazerHydraAxis(RazerHydraAxis.TYPE.JOYSTICK_X, JoystickPressThreshold) },
                { RazerHydraAxis.TYPE.JOYSTICK_Y, new RazerHydraAxis(RazerHydraAxis.TYPE.JOYSTICK_Y, JoystickPressThreshold) },
                { RazerHydraAxis.TYPE.TRIGGER, new RazerHydraAxis(RazerHydraAxis.TYPE.TRIGGER, TriggerPressThreshold) }
            };
        }

        void Update()
        {
            currentData = RazerHydraManager.Instance.GetControllerData((int)ControllerID);
            foreach(var element in buttons.Values)
            {
                element.Update(currentData);
            }
            foreach (var element in axis.Values)
            {
                element.Update(currentData);
            }
        }

        public Vector3 GetPosition()
        {
            var pos = currentData.pos;
            var newPos = new Vector3(pos.x, pos.y, -pos.z);
            return newPos * scaleFactor;
        }

        public Quaternion GetRotation()
        {
            var rot = currentData.rot_quat;
            return new Quaternion(-rot.x, -rot.y, rot.z, rot.w);
        }

        public bool GetButtonDown(RazerHydraButton.TYPE type)
        {
            return buttons[type].IsDown();
        }

        public bool GetButtonDown(RazerHydraAxis.TYPE type)
        {
            return axis[type].IsDown();
        }

        public bool GetButtonUp(RazerHydraButton.TYPE type)
        {
            return buttons[type].IsUp();
        }

        public bool GetButtonUp(RazerHydraAxis.TYPE type)
        {
            return axis[type].IsUp();
        }

        public bool GetButton(RazerHydraButton.TYPE type)
        {
            return buttons[type].IsPressed();
        }

        public bool GetButton(RazerHydraAxis.TYPE type)
        {
            return axis[type].IsPressed();
        }

        public float GetAxis(RazerHydraAxis.TYPE type)
        {
            return axis[type].Value;
        }
    }
}