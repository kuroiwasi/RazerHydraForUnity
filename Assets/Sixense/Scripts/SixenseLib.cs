using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace Sixense
{
    // Reference:
    // - https://forum.unity.com/threads/sixense-truemotion-hyrda-controllers.89579
    public static class SixenseLib
    {
        public const int SIXENSE_SUCCESS = 0;
        public const int SIXENSE_FAILURE = -1;
        private const string libName = "sixense_x64";

        public enum SIXENSE_BUTTON
        {
            START = 0x01 << 0,
            BUTTON_1 = 0x01 << 5,
            BUTTON_2 = 0x01 << 6,
            BUTTON_3 = 0x01 << 3,
            BUTTON_4 = 0x01 << 4,
            BUMPER = 0x01 << 7,
            JOYSTICK = 0x01 << 8
        }

        public enum SIXENSE_HAND_TYPE
        {
            UNDEFINED = 0,
            LEFT = 1,
            RIGHT = 2
        }

        public struct SixenseControllerData
        {
            public Vector3 pos;
            public Vector3 rot_mat_x;
            public Vector3 rot_mat_y;
            public Vector3 rot_mat_z;
            public float joystick_x;
            public float joystick_y;
            public float trigger;
            public uint buttons;
            public byte sequence_number;
            public Quaternion rot_quat;
            public short firmware_revision;
            public short hardware_revision;
            public short packet_type;
            public short magnetic_frequency;
            public int enabled;
            public int controller_index;
            public byte is_docked;
            public byte which_hand;
            public byte hemi_tracking_enabled;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SixenseAllControllerData
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public SixenseControllerData[] controllers;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SixenseControllerDataHistory
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public SixenseControllerData[] controllers;
        }

        [DllImport(libName, EntryPoint = "sixenseInit")]
        public static extern int Init();

        [DllImport(libName, EntryPoint = "sixenseExit")]
        public static extern int Exit();

        [DllImport(libName, EntryPoint = "sixenseGetMaxBases")]
        public static extern int GetMaxBases();

        [DllImport(libName, EntryPoint = "sixenseSetActiveBase")]
        public static extern int SetActiveBase(int base_num);

        [DllImport(libName, EntryPoint = "sixenseIsBaseConnected")]
        public static extern int IsBaseConnected(int base_num);

        [DllImport(libName, EntryPoint = "sixenseGetMaxControllers")]
        public static extern int GetMaxControllers();

        [DllImport(libName, EntryPoint = "sixenseGetNumActiveControllers")]
        public static extern int GetNumActiveControllers();

        [DllImport(libName, EntryPoint = "sixenseIsControllerEnabled")]
        public static extern int IsControllerEnabled(int which);

        [DllImport(libName, EntryPoint = "sixenseGetAllNewestData")]
        public static extern int GetAllNewestData(out SixenseAllControllerData all_data);

        [DllImport(libName, EntryPoint = "sixenseGetAllData")]
        private static extern int GetAllData_(int index_back, out SixenseControllerDataHistory all_data);

        [DllImport(libName, EntryPoint = "sixenseGetNewestData")]
        public static extern int GetNewestData(int which, out SixenseControllerData data);

        [DllImport(libName, EntryPoint = "sixenseGetData")]
        private static extern int GetData_(int which, int index_data, out SixenseControllerDataHistory data);

        [DllImport(libName, EntryPoint = "sixenseGetHistorySize")]
        public static extern int GetHistorySize();

        [DllImport(libName, EntryPoint = "sixenseSetFilterEnabled")]
        public static extern int SetFilterEnabled(int on_or_off);

        [DllImport(libName, EntryPoint = "sixenseGetFilterEnabled")]
        public static extern int GetFilterEnabled(out int on_or_off);

        [DllImport(libName, EntryPoint = "sixenseSetFilterParams")]
        public static extern int SetFilterParams(float near_range, float near_val, float far_range, float far_val);

        [DllImport(libName, EntryPoint = "sixenseGetFilterParams")]
        public static extern int GetFilterParams(out float near_range, out float near_val, out float far_range, out float far_val);

        [DllImport(libName, EntryPoint = "sixenseTriggerVibration")]
        public static extern int TriggerVibration(int controller_id, int duration_100ms, int pattern_id);

        [DllImport(libName, EntryPoint = "sixenseAutoEnableHemisphereTracking")]
        public static extern int AutoEnableHemisphereTracking(int which_controller);

        [DllImport(libName, EntryPoint = "sixenseSetHighPriorityBindingEnabled")]
        public static extern int SetHighPriorityBindingEnabled(int on_or_off);

        [DllImport(libName, EntryPoint = "sixenseGetHighPriorityBindingEnabled")]
        public static extern int GetHighPriorityBindingEnabled(out int on_or_off);

        [DllImport(libName, EntryPoint = "sixenseSetbaseColor")]
        public static extern int SetbaseColor(char red, char green, char blue);

        [DllImport(libName, EntryPoint = "sixenseGetBaseColor")]
        public static extern int GetBaseColor(out char red, out char green, out char blue);

        public static int GetAllData(int index_back, out SixenseControllerDataHistory dataHistory)
        {
            SixenseControllerDataHistory tmp = new SixenseControllerDataHistory();
            int result = GetAllData_(index_back, out tmp);
            dataHistory.controllers = new SixenseControllerData[index_back * 4];
            Array.Copy(tmp.controllers, dataHistory.controllers, index_back * 4);
            return result;
        }

        public static int GetData(int which, int index_back, out SixenseControllerDataHistory dataHistory)
        {
            SixenseControllerDataHistory tmp = new SixenseControllerDataHistory();
            int result = GetData_(which, index_back, out tmp);
            dataHistory.controllers = new SixenseControllerData[index_back];
            Array.Copy(tmp.controllers, dataHistory.controllers, index_back);
            return result;
        }
    }
}