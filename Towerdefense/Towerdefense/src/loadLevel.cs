using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml.Linq;

namespace Towerdefense
{
    class LoadLevel
    {
        private List<Wave> waves = null;

        private List<Field> grid = null;

        int grid_count;

        public void load(String str)
        {
            System.IO.Stream stream = TitleContainer.OpenStream(str);

            XDocument doc = XDocument.Load(stream);

            waves = new List<Wave>();

            grid = new List<Field>();

            waves = (from wave in doc.Descendants("wave")
                     select new Wave()
                     {
                         Count = Convert.ToInt32(wave.Element("count").Value),
                         Enemy = (from e in wave.Descendants("enemy") select new Enemy()
                         {
                             HitPoints = Convert.ToInt32(e.Element("hitPoints").Value),
                             MovementSpeed = Convert.ToInt32(e.Element("movementSpeed").Value),
                             Resistance = e.Element("resistance").Value,
                             IsBoss=Convert.ToBoolean(e.Element("boss").Value),
                             IsFlying=Convert.ToBoolean(e.Element("flying").Value)                             
                         }).ToList()[0]
                     }).ToList();

            grid = (from field in doc.Descendants("field")
                    select new Field()
                    {
                        X = Convert.ToInt32(field.Element("x").Value),
                        Y = Convert.ToInt32(field.Element("Y").Value),
                        Type = field.Element("type").Value,
                        Rotation = Convert.ToInt32(field.Element("rotation").Value)
                    }).ToList();

            grid_count = Convert.ToInt32(doc.Element("grid").Element("count").Value);
        }

        public List<Field> getGrid()
        {
            return grid;
        }

        public List<Wave> getWaves()
        {

            return waves;            

        }

        public int getGridCount()
        {
            return grid_count;
        }
    }
}
