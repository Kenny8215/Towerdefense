using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml;

namespace Towerdefense
{
    class SaveLevel
    {

        internal void save(Vector2[,] roadTypeAndRotation)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


            XmlDocument doc = new XmlDocument();
            XmlNode myRoot;

            //create root
            myRoot = doc.CreateElement("level");
            doc.AppendChild(myRoot);

            //create waves
            XmlNode waves = doc.CreateElement("waves");
            myRoot.AppendChild(waves);

            //create wave 1
            XmlNode wave = doc.CreateElement("wave");

            //enym count
            XmlNode count = doc.CreateElement("count");
            count.InnerText = "10";
            wave.AppendChild(count);

            XmlNode enemy = this.createEnemyNote("wolf", 100, 1, "enemies/wolf", "ice", false, false,doc);
            wave.AppendChild(enemy);

            waves.AppendChild(wave);

            //create wave 2
            wave = doc.CreateElement("wave");

            //enym count
            count = doc.CreateElement("count");
            count.InnerText = "10";
            wave.AppendChild(count);

            enemy = this.createEnemyNote("fish", 1000, 10, "enemies/wolf", "ice", false, true, doc);
            wave.AppendChild(enemy);

            waves.AppendChild(wave);


            XmlNode grid = doc.CreateElement("grid");

            count = doc.CreateElement("count");
            count.InnerText = roadTypeAndRotation.GetLength(0).ToString();
            grid.AppendChild(count);

            XmlNode field;
            XmlNode x;
            XmlNode y;
            XmlNode type;
            XmlNode rotation;
            for(int i = 0; i < roadTypeAndRotation.GetLength(0); i++)
            {
                for(int j = 0; j < roadTypeAndRotation.GetLength(1); j++)
                {
                    field = doc.CreateElement("field");
                    x = doc.CreateElement("x");
                    x.InnerText = i.ToString();

                    y = doc.CreateElement("y");
                    y.InnerText = j.ToString();

                    type = doc.CreateElement("type");
                    type.InnerText = roadTypeAndRotation[i, j].X.ToString();

                    rotation = doc.CreateElement("rotation");
                    rotation.InnerText = roadTypeAndRotation[i, j].Y.ToString();                   
                    

                    field.AppendChild(x);
                    field.AppendChild(y);
                    field.AppendChild(type);
                    field.AppendChild(rotation);

                    grid.AppendChild(field);
                }
                
            }

            myRoot.AppendChild(grid);

            XmlNode tower = doc.CreateElement("tower");

            XmlNode tow = doc.CreateElement("tow");
            

            XmlNode range = doc.CreateElement("range");
            range.InnerText = "5";
            tow.AppendChild(range);

            XmlNode cost = doc.CreateElement("cost");
            cost.InnerText = "50";
            tow.AppendChild(cost);

            XmlNode damge = doc.CreateElement("damage");
            damge.InnerText = "5";
            tow.AppendChild(damge);

            XmlNode firerate = doc.CreateElement("rate");
            firerate.InnerText = "5";
            tow.AppendChild(firerate);

            XmlNode speed = doc.CreateElement("speed");
            speed.InnerText = "5";
            tow.AppendChild(speed);

            XmlNode isUpgradeable = doc.CreateElement("upgrade");
            isUpgradeable.InnerText = "false";
            tow.AppendChild(isUpgradeable);

            tower.AppendChild(tow);

            myRoot.AppendChild(tower);

            doc.Save(@desktopPath+"/custom_lvl.xml");
        }

        private XmlNode createEnemyNote(string name, int hitPoints, int speed, string sprite, string resistance, bool boss, bool flying, XmlDocument doc)
        {
            XmlNode enemy = doc.CreateElement("enemy");

            XmlNode na = doc.CreateElement("name");
            na.InnerText = name;
            enemy.AppendChild(na);

            XmlNode hp = doc.CreateElement("hitPoints");
            hp.InnerText = hitPoints.ToString();
            enemy.AppendChild(hp);

            XmlNode spe = doc.CreateElement("movementSpeed");
            spe.InnerText = speed.ToString();
            enemy.AppendChild(spe);

            XmlNode spr = doc.CreateElement("sprite");
            spr.InnerText = sprite;
            enemy.AppendChild(spr);

            XmlNode res = doc.CreateElement("resistance");
            res.InnerText = resistance;
            enemy.AppendChild(res);

            XmlNode bos = doc.CreateElement("boss");
            bos.InnerText = boss.ToString();
            enemy.AppendChild(bos);

            XmlNode fl = doc.CreateElement("flying");
            fl.InnerText = flying.ToString();
            enemy.AppendChild(fl);

            return enemy;
        }
    }
}
