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
        #region Fields
        private List<Wave> waves = null;

        private List<Field> grid = null;

        private List<Tower> tower1 = null;

        int grid_count;
        #endregion

        #region Load
        public void load(String str)
        {
            System.IO.Stream stream = TitleContainer.OpenStream(str); //create stream for reading the xml file

            XDocument doc = XDocument.Load(stream);

            waves = new List<Wave>();

            grid = new List<Field>();

            waves = (from wave in doc.Descendants("wave")
                     select new Wave()
                     {
                         Count = Convert.ToInt32(wave.Element("count").Value),//read out size of the wave
                         Enemy = (from e in wave.Descendants("enemy")
                                  select new Enemy() //read out all enemys of a wave
                                      {
                                          HitPoints = Convert.ToInt32(e.Element("hitPoints").Value),
                                          MovementSpeed = Convert.ToInt32(e.Element("movementSpeed").Value),
                                          Resistance = e.Element("resistance").Value,
                                          IsBoss = Convert.ToBoolean(e.Element("boss").Value),
                                          IsFlying = Convert.ToBoolean(e.Element("flying").Value),
                                          SpritePath = "enemies/wolf",
                                          HealthBarPath = "enemies/health"
                                      }).ToList()[0]
                     }).ToList();

            grid = (from field in doc.Descendants("field")
                    select new Field()
                    {
                        X = Convert.ToInt32(field.Element("x").Value), //read out all the fields
                        Y = Convert.ToInt32(field.Element("y").Value),
                        Type = Convert.ToInt32(field.Element("type").Value),
                        Rotation = Convert.ToInt32(field.Element("rotation").Value)
                    }).ToList();

            tower1 = (from tower in doc.Descendants("tow")
                      select new Tower()
                      {
                          Range = Convert.ToInt32(tower.Element("range").Value),
                          Cost = Convert.ToInt32(tower.Element("cost").Value),
                          Damage = Convert.ToInt32(tower.Element("damage").Value),
                          FireRate = Convert.ToInt32(tower.Element("rate").Value),
                          Speed = Convert.ToInt32(tower.Element("speed").Value),
                          IsUpgradeable = Convert.ToBoolean(tower.Element("upgrade").Value),
                          UpgradeCost = Convert.ToInt32(tower.Element("upgradecost").Value),
                          RangeCirclePath = "rangeCircle",
                          UpdatePath = "Tower/upgradeGreen",
                          SpritePath = tower.Element("path").Value,
                          WeaponPath="Tower/viking_axe"
                      }).ToList();
            
            XElement g = doc.Descendants("grid").ElementAt(0);
            grid_count = Convert.ToInt32(g.Element("count").Value);//read out size of the field
            foreach(Wave w in waves)
            {
                w.loadDone();
            }
          
        }
        #endregion
        #region Setter and Getter
        public List<Field> getGrid()
        {
            return grid;
        }

        public List<Wave> getWaves()
        {

            return waves;            

        }

        public List<Tower> getTower()
        {
            return tower1;
        }

        public int getGridCount()
        {
            return grid_count;
        }
        #endregion
    }
}
