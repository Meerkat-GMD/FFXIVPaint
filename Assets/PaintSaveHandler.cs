using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public struct PaintObjectSaveData
    {
        public Vector3 LocalPosition;
        public Quaternion Rotation;
        public Vector2 Size;

        public PaintObjectSaveData(GameObject gameObject)
        {
            LocalPosition = gameObject.transform.localPosition;
            Rotation = gameObject.transform.rotation;
            Size = gameObject.GetComponent<RectTransform>().sizeDelta;
        }
    }
    
    public class PaintSaveHandler : MonoBehaviour
    {
        [SerializeField] private GameObject always;

        private string Path = $"{Application.dataPath}/SavaData/";
        
        public void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.J))
            {
                Load();
            }
        }

        private void Save()
        {
            var sb = new StringBuilder();
            JsonSerializerSettings setting = new JsonSerializerSettings(); 
            setting.Formatting = Formatting.Indented; 
            setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            int childCount = always.transform.childCount;
            sb.Append("[\n");
            for (int i = 0; i < childCount; i++)
            {
                var alwaysObject = always.transform.GetChild(i);
                var saveData = new PaintObjectSaveData(alwaysObject.gameObject);
                var jsonData = JsonConvert.SerializeObject(saveData, setting);

                sb.Append(jsonData);
                if (i != childCount - 1)
                {
                    sb.Append(",\n");
                }
            }
            sb.Append("\n]");
            
            System.IO.FileInfo fileInfo = new (Path); 
            fileInfo.Directory.Create();
            System.IO.File.WriteAllText(Path + @"/data.json", sb.ToString());
        }

        private void Load()
        {
            var rawData = System.IO.File.ReadAllText(Path + "/data.json");
            var paintObjectSaveDatas = JsonConvert.DeserializeObject<List<PaintObjectSaveData>>(rawData);
            
            int childCount = always.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                if (i >= paintObjectSaveDatas.Count)
                {
                    break;
                }
                
                var alwaysObject = always.transform.GetChild(i);
                var paintObjectData = paintObjectSaveDatas[i];

                alwaysObject.localPosition = paintObjectData.LocalPosition;
                alwaysObject.rotation = paintObjectData.Rotation;
                alwaysObject.gameObject.GetComponent<RectTransform>().sizeDelta = paintObjectData.Size;
            }
        }
    }
}