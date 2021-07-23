using static Sixense.SixenseLib;

namespace Sixense
{
    public class RazerHydraManager : SingletonMonoBehaviour<RazerHydraManager>
    {
        private SixenseAllControllerData AllData;
        private bool initialized;

        new void Awake()
        {
            base.Awake();
            if (isActiveAndEnabled)
            {
                Init();
                GetAllNewestData(out AllData);
                initialized = true;
            }
        }

        new void OnDestroy()
        {
            if (initialized)
            {
                Exit();
                base.OnDestroy();
            }
        }

        void Update()
        {
            GetAllNewestData(out AllData);
        }

        public SixenseControllerData GetControllerData(int index)
        {
            foreach (var controller in AllData.controllers)
            {
                if (controller.controller_index == index) return controller;
            }
            return new SixenseControllerData();
        }
    }
}