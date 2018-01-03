using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Syroot.NintenTools.Byaml.Dynamic;
using Syroot.NintenTools.Byaml.Serialization;

namespace Syroot.NintenTools.Byaml
{
    class Program
    {
        static Predicate<Dictionary<string, object>> HasName(string name)
        {
            return dictionary => dictionary.ContainsKey("name") && dictionary["name"] as string == name;
        }

        static Action<Dictionary<string, object>> Set(string key, string value)
        {
            return dictionary => dictionary[key] = value;
        }

        static void Main(string[] args)
        {
            Dictionary<string, dynamic> byamlData = ByamlFile.Load("ActorInfo.product.byml");
            ((List<dynamic>)byamlData["Actors"]).ConvertAll<Dictionary<string, object>>(actor => actor as Dictionary<string, object>)
                                               .FindAll(HasName("Weapon_Sword_018"))
                                               .ForEach(Set("attackPower", "8888"));

            List<Dictionary<string, object>> weapon = ((List<dynamic>)byamlData["Actors"]).ConvertAll<Dictionary<string, object>>(actor => actor as Dictionary<string, object>)
                                                                                          .FindAll(HasName("Weapon_Sword_018"));

            ByamlFile.Save("ActorInfo.product_test.byml", byamlData);
        }
    }
}
