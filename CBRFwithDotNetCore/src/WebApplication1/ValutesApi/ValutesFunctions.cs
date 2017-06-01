using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ValutesApi
{
    public class ValutesFunctions : IValutesFunctions
    {
       public  List<Valutes> ValsList = new List<Valutes>();

        public void LoadValutes()
        {
            
        }

        public Valutes Create(string _name, string _exchange, int _worldName, string _chCode)
        {
            Valutes tmp = new Valutes();
            tmp.Name = _name;
            tmp.Exchange = _exchange;
            tmp.WorldName = _worldName;
            tmp.ChCode = _chCode;
            tmp.IsChecked =false;
            tmp.HowMuch = -1;

            ValsList.Add(tmp);
            return tmp;
        }

        public IEnumerable<Valutes> GetAll()
        {
            return ValsList;
        }

        public void Update(Valutes chngVal)
        {
            var valToUpdate = ValsList.SingleOrDefault(r => r.ChCode == chngVal.ChCode);
            if (valToUpdate != null)
            {
                valToUpdate.Exchange = chngVal.Exchange;
                //....
            }
        }

        public int getValNumber(string valName)
        {
            for (int i=0; i<ValsList.Count; i++)
                if (ValsList[i].Name.Trim() == valName.Trim())
                    return i;
            return -1;
        }

        public Dictionary<string, string> getChecked()
        {
            Dictionary<string, string> tmp = new Dictionary<string, string>();
            foreach (Valutes curVal in ValsList)
            
                if (curVal.IsChecked)
                    tmp.Add(curVal.Name, curVal.Exchange);           
            return tmp;
        }

        public List<string> getNames()
        {
            List<string> answer = new List<string>();
            for(int i = 0; i < ValsList.Count; i++)
                answer.Add(ValsList[i].Name.Trim());
                return answer;
        }

        //возвращает удалось ли изменить значение
        public bool CheckValute(string valName)
        {
            int tmp = getValNumber(valName);
            if (tmp > -1)
            {
                ValsList[tmp].IsChecked = !ValsList[tmp].IsChecked;
                return true;
            }
            return false;
        }

        //возвращает отметку checked
        public bool isChecked(string valName)
        {
            for (int i = 0; i < ValsList.Count; i++)
                if (ValsList[i].Name.Trim() == valName.Trim())
                    return ValsList[i].IsChecked;
            return false;
        }
    }
}
