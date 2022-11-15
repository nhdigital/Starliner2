using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NHDigital.Scripts
{
    public class NHDLogger
    {
#if DEBUG
        GUIStyle m_guiStyle = new GUIStyle();
        string m_logText = string.Empty;
        List<string> m_logLines = new List<string>();
#endif

#if DEBUG
        public void OnGUI()
        {
            lock (m_logText)
            {
                DrawTextWithShadow(10, 50, 800, 500, m_logText);
            }
        }
#endif

#if DEBUG
        private void DrawTextWithShadow(float x, float y, float width, float height, string text)
        {
            m_guiStyle.fontSize = 20;
            m_guiStyle.normal.textColor = Color.black;
            GUI.Label(new UnityEngine.Rect(x, y, height, height), text, m_guiStyle);
            m_guiStyle.normal.textColor = Color.white;
            GUI.Label(new UnityEngine.Rect(x - 1, y - 1, width, height), text, m_guiStyle);
        }
#endif

        public void Write(string line)
        {
#if DEBUG
            lock (m_logText)
            {
                if (m_logLines.Count > 19)
                {
                    m_logLines.RemoveAt(0);
                }
                m_logLines.Add(DateTime.Now.ToString("HH:mm:ss.ff") + "  " + line);

                m_logText = string.Empty;
                foreach (string s in m_logLines)
                {
                    m_logText += "\n";
                    m_logText += s;
                }
            }
#endif
        }

    }
}