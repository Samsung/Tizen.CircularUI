/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Runtime.InteropServices;

namespace Tizen.Wearable.CircularUI.Forms.Renderer.Interop
{
    static class EFL
    {
        const string Ecore_IMF = "libecore_imf.so.1";
        const string Elementary = "libelementary.so.1";

        //enum Ecore_IMF_Input_Panel_Event
        internal enum InputPanelEventType
        {
            State,
            Language,
            ShiftMode,
            Geometry,
            CandidateState,
            CandidateGeometry,
            KeyboardMode
        };

        // enum Ecore_IMF_Input_Panel_State
        internal enum InputPanelState
        {
            Show,
            Hide,
            WillShow
        };

        internal delegate void InputPanelEventCallback(IntPtr data, IntPtr ctx, int value);

        [DllImport(Ecore_IMF)]
        internal static extern void ecore_imf_context_input_panel_event_callback_add(IntPtr ctx, InputPanelEventType type, InputPanelEventCallback func, IntPtr data);

        [DllImport(Elementary)]
        internal static extern IntPtr elm_entry_imf_context_get(IntPtr entryHandle);
    }
}
