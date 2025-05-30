using GeckoDexModelsLibrary;
using static GeckoDexModelsLibrary.Component;
using static GeckoDexModelsLibrary.Kibble;
using System.Text.Json;
using static GeckoDexModelsLibrary.Narcotic;

namespace GeckoDexWPFApp
{
    public class GetElementFromJSON
    {
        public static Kibble GetKibbleFromJson(IEnumerable<JsonElement> kibbles, int kibbleId)
        {
            foreach (JsonElement kibb in kibbles)
            {
                if (kibb.GetProperty("Id").GetInt32() == kibbleId)
                {
                    List<Component> components = new();

                    foreach (JsonElement comp in kibb.GetProperty("Recipe").GetProperty("Components").EnumerateArray())
                    {
                        ComponentBuilder compBuilder = new ComponentBuilder()
                            .SetId(comp.GetProperty("Id").GetInt32())
                            .SetName(comp.GetProperty("Name").GetString())
                            .SetQuantity(comp.GetProperty("Quantity").GetInt32())
                            .SetImagePath(comp.GetProperty("ImagePath").GetString())
                            .SetDescription(comp.GetProperty("Description").GetString());

                        components.Add(compBuilder.Build());
                    }

                    Recipe recipe = new Recipe(components);

                    KibbleBuilder builder = new KibbleBuilder()
                        .SetId(kibbleId)
                        .SetName(kibb.GetProperty("Name").GetString())
                        .SetDescription(kibb.GetProperty("Description").GetString())
                        .SetImagePath(kibb.GetProperty("ImagePath").GetString())
                        .SetRecipe(recipe)
                        .SetKibbleType(Enum.Parse<KibbleType>(kibb.GetProperty("KibbleType").GetString()))
                        .SetTamingEffectiveness(kibb.GetProperty("TamingEffectiveness").GetInt32())
                        .SetFoodPoints(kibb.GetProperty("FoodPoints").GetInt32());

                    return builder.Build();
                }
            }

            // Si aucun trouvé
            return new Kibble();
        }

        public static Narcotic GetNarcoticFromJson(IEnumerable<JsonElement> narcos, int narcoticId)
        {
            foreach (JsonElement narco in narcos)
            {
                if (narco.GetProperty("Id").GetInt32() == narcoticId)
                {
                    List<Component> components = new();

                    foreach (JsonElement comp in narco.GetProperty("Recipe").GetProperty("Components").EnumerateArray())
                    {
                        ComponentBuilder compBuilder = new ComponentBuilder()
                            .SetId(comp.GetProperty("Id").GetInt32())
                            .SetName(comp.GetProperty("Name").GetString())
                            .SetQuantity(comp.GetProperty("Quantity").GetInt32())
                            .SetImagePath(comp.GetProperty("ImagePath").GetString())
                            .SetDescription(comp.GetProperty("Description").GetString());

                        components.Add(compBuilder.Build());
                    }

                    Recipe recipe = new Recipe(components);

                    NarcoticBuilder builder = new NarcoticBuilder()
                        .SetId(narcoticId)
                        .SetName(narco.GetProperty("Name").GetString())
                        .SetDescription(narco.GetProperty("Description").GetString())
                        .SetImagePath(narco.GetProperty("ImagePath").GetString())
                        .SetRecipe(recipe)
                        .SetTorpidity(narco.GetProperty("Torpidity").GetInt32());

                    return builder.Build();
                }
            }

            // Si aucun trouvé
            return new Narcotic();
        }
    }
}
