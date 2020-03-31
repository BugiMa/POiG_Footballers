using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_1C
{
    internal class Footballer
    {
       
        #region Prop
        public string Name { get; set; }
        public string Surname { get; set; }
        public uint Age { get; set; }
        public uint Weight { get; set; }
        #endregion

        #region constr
        public Footballer(string name, string surname, uint age, uint weight)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Weight = weight;
        }
        public Footballer(string Name, string Surname) : this(Name, Surname, 30, 75) { }
        #endregion

        #region methods
        public bool isTheSame(Footballer pilkarz)
        {
            if (pilkarz.Surname != Surname) return false;
            if (pilkarz.Name != Name) return false;
            if (pilkarz.Age != Age) return false;
            if (pilkarz.Weight != Weight) return false;
            return true;
        }

        public override string ToString()
        {
            return $"{Surname} {Name} lat: {Age} waga: {Weight} kg";
        }

        public string ToFileFormat()
        {
            return $"{Surname}|{Name}|{Age}|{Weight}";
        }

        public static Footballer CreateFromString(string sFootballer)
        {
            string Name, Surname;
            uint Age, Weight;
            var pola = sFootballer.Split('|');
            if(pola.Length==4)
            {
                Surname = pola[1];
                Name = pola[0];
                Age = uint.Parse(pola[2]);
                Weight = uint.Parse(pola[3]);
                return new Footballer(Name, Surname, Age, Weight);
            }
            throw new Exception("Błędny format danych z pliku");
        }
        #endregion
    }
}
