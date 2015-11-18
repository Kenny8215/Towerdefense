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
            /*
            string note = "";
            string prevNote = "";

            int x = 0;
            int y = 0;
            string type = "";
            int rot = 0;

            Enemy currEnemy = null;
            Wave currWave = null;            

            int waveCount = 0;

            System.IO.StreamReader XMLread = new System.IO.StreamReader(@str);

            using (XmlReader reader = XmlReader.Create(XMLread))
            {
                
                    // Parse the file and display each of the nodes.
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                            //startelement

                                prevNote = note;
                                note = reader.Name;    
                                                   
                            break;


                            case XmlNodeType.Text:
                            //the text in the element
                            switch (note)
                            {
                                case "level":
                                    break;

                                case "waves":
                                    waves = new SortedList<int, Wave>();
                                    break;

                                case "wave":
                                    currWave = new Wave();
                                    waveCount++;
                                    break;

                                case "count":

                                    if (prevNote == "wave")
                                    {
                                        currWave.Count = Int32.Parse(reader.Value);
                                    }else if (prevNote == "grid")
                                    {
                                        int tmp = Int32.Parse(reader.Value);
                                        grid = new int[tmp, tmp, 2];
                                    }

                                    break;

                                case "enemy":
                                    currEnemy = new Enemy();
                                    break;

                                case "name":
                                    //currEnemy.ID1 = reader.Value;
                                    break;

                                case "hitPoints":
                                    currEnemy.HitPoints = Int32.Parse(reader.Value);
                                    break;

                                case "movementSpeed":
                                    currEnemy.MovementSpeed = Int32.Parse(reader.Value);
                                    break;

                                case "sprite":
                                    //currEnemy.setSprite(reader.Value);
                                    break;

                                case "resistance":
                                    currEnemy.Resistance = reader.Value;
                                    break;

                                case "boss":
                                    if (reader.Value == "false")
                                    {
                                        currEnemy.IsBoss = false;
                                    }
                                    else
                                    {
                                        currEnemy.IsBoss = true;
                                    }
                                    break;

                                case "flying":
                                    if (reader.Value == "false")
                                    {
                                        currEnemy.IsFlying = false;
                                    }
                                    else
                                    {
                                        currEnemy.IsFlying = true;
                                    }
                                    break;

                                case "grid":
                                    break;

                                case "field":
                                    break;

                                case "type":
                                    type = reader.Value;
                                    break;

                                case "rotation":
                                    rot = Int32.Parse(reader.Value);
                                    break;

                                case "x":
                                    x = Int32.Parse(reader.Value);
                                    break;

                                case "y":
                                    y = Int32.Parse(reader.Value);
                                    break;
                            }
                            break;
                            case XmlNodeType.XmlDeclaration:

                            break;

                            case XmlNodeType.ProcessingInstruction:
                                //no idea
                                break;

                            case XmlNodeType.Comment:
                                //comment
                                break;

                            case XmlNodeType.EndElement:
                            //the end of an element

                            switch (reader.Name)
                            {
                                case "/level":
                                    break;

                                case "/waves":
                                    break;

                                case "/wave":
                                    waves.Add(waveCount, currWave);
                                    break;

                                case "/count":
                                    break;

                                case "/enemy":
                                    currWave.Enemy = currEnemy;
                                    break;

                                case "/name":
                                    break;

                                case "/hitPoints":
                                    break;

                                case "/movementSpeed":
                                    break;

                                case "/sprite":
                                    break;

                                case "/resistance":
                                    break;

                                case "/boss":
                                    break;

                                case "/flying":
                                    break;

                                case "/grid":
                                    break;

                                case "/field":
                                    if (type == "start")
                                    {
                                        grid[x, y, 0] = 1;
                                    }
                                    
                                    grid[x, y, 1] = rot;
                                    break;

                                case "/type":
                                    break;

                                case "/rotation":
                                    break;
                            }


                            break;
                        }
                    }
                
            }*/


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
