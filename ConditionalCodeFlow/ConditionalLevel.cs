using System;
using System.Collections.Generic;
using System.Text;

namespace ConditionalCore
{
    public class ConditionalLevel
    {
        private int levelId;
        public Dictionary<string, ConditionalService> levelServices = new Dictionary<string, ConditionalService>();

        public string getName()
        {
            return this.GetType().Name;
        }

        public bool AddService(ConditionalService service) {
            if (!levelServices.ContainsKey(service.getName())) {
                levelServices.Add(service.getName(), service);
                return true;
            }
            return false; 
        }

        public void setLevelId(int id)
        {
            levelId = id;
        }

        public int getLevelId()
        {
            return levelId;
        }
    }
}
