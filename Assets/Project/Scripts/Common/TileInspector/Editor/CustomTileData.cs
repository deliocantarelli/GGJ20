using UnityEngine;
using UnityEditor;
using System.Collections;

namespace PointNSheep.Common.TileInspector
{
    [CustomPropertyDrawer(typeof(TileData))]
    public class CustomTileData : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PrefixLabel(position, label);

            Rect newPosition = position;
            newPosition.y += 18f;
            SerializedProperty rows = property.FindPropertyRelative("rows");

            for (int i = 0; i < 7; i++)
            {
                SerializedProperty row = rows.GetArrayElementAtIndex(i).FindPropertyRelative("row");
                newPosition.height = 20;

                if (row.arraySize != 7)
                    row.arraySize = 7;

                newPosition.width = 60;

                for (int j = 0; j < 7; j++)
                {
                    EditorGUI.PropertyField(newPosition, row.GetArrayElementAtIndex(j), GUIContent.none);
                    newPosition.x += newPosition.width;
                }

                newPosition.x = position.x;
                newPosition.y += 20;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 20 * 12;
        }
    }
}