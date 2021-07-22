using static Sixense.SixenseLib;

namespace Sixense
{
    public class RazerHydraManager : SingletonMonoBehaviour<RazerHydraManager>
    {
        private SixenseAllControllerData AllData;

        void OnEnable()
        {
            Init();
            GetAllNewestData(out AllData);
        }

        private void OnApplicationQuit()
        {
            Exit();
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