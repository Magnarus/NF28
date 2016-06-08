using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using Game;
using UnityEngine;

namespace Descriptors
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Descriptor), true)]
    public class DescriptorEditor : Editor
    {
        public override void OnInspectorGUI()
        {

            if (base.GetType() != typeof(Component))
                base.OnInspectorGUI();

            Type derived = target.GetType();

            foreach(var gv in 
                derived.GetFields()
                .Where(x => (x.FieldType.IsGenericType ? x.FieldType : x.FieldType.BaseType.IsGenericType ? x.FieldType.BaseType : typeof(List<string>)).GetGenericTypeDefinition() == typeof(GameValue<>)))
            {
                object fieldAccess = gv.GetValue(target);
                if (fieldAccess == null)
                    gv.SetValue(target, fieldAccess = gv.FieldType.GetConstructor(new Type[] { }).Invoke(new object[] { }));


                var isSimpleGameValue = gv.GetCustomAttributes(typeof(SimpleGameValueAttribute), true).Any();
                
                var MaxValueProperty = fieldAccess.GetType().GetProperty("BaseValue");
                var CurrentValueProperty = fieldAccess.GetType().GetProperty("CurrentValue");
                Type gvType = (gv.FieldType.IsGenericType ? gv.FieldType : gv.FieldType.BaseType).GetGenericArguments()[0];  

                var MaxValue = MaxValueProperty.GetValue(fieldAccess, null);
                var CurrentValue = CurrentValueProperty.GetValue(fieldAccess, null);
                //Créer un champs de texte, fais un reverse ToString dessus (ou crash si le type de la GameValue n'est pas IConvertible depuis un string mais personne ne ferait ça)
                if (!isSimpleGameValue)
                {
                    MaxValueProperty.SetValue(fieldAccess, Convert.ChangeType(
                        EditorGUILayout.TextField(gv.Name + " Max", MaxValue != null ? MaxValue.ToString() : ""),
                        gvType), null);
                    CurrentValueProperty.SetValue(fieldAccess, Convert.ChangeType(
                        EditorGUILayout.TextField(gv.Name + " Actuel", CurrentValue != null ? CurrentValue.ToString() : ""),
                        gvType), null);
                }
                else
                {
                    MaxValueProperty.SetValue(fieldAccess, Convert.ChangeType(
                        EditorGUILayout.TextField(gv.Name, MaxValue != null ? MaxValue.ToString() : ""),
                        gvType), null);
                }
            }
        }
    }
}
