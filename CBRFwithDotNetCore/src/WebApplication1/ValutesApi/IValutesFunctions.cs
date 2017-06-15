using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CBRFConverter.Models;

namespace CBRFConverter.ValutesApi
{
    public interface IValutesFunctions
    {
        void LoadValutes();

        Valutes Create(string _name, string _exchange, int _worldName, string _chCode);
        IEnumerable<Valutes> GetAll();
        void Update(Valutes chngVal);
        bool CheckValute(string valName);
        int getValNumber(string valName);
        bool isChecked(string valName);
        Dictionary<string, string> getChecked();
        List<string> getNames();

    }
}
