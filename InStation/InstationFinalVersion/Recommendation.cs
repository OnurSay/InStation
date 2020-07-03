using InstationFinalVersion.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstationFinalVersion
{
    public class Recommendation
    {
        public List<Channel> Reccommendation(List<UserHistory> History)
        {
            List<RecommendationWeight> weights = Weights(History);
            DA_Channel channel = new DA_Channel();
            //Get Channels according to the weights
            List<Channel> ChannelList = channel.GetChannelsWithWeights(weights);

            //Return Ch1annel List for recommendation
            return ChannelList;
        }

        public List<RecommendationWeight> Weights(List<UserHistory> History)
        {
            List<RecommendationWeight> weightList = new List<RecommendationWeight>();
            WeightReturner weightReturner = new WeightReturner();

            for (int i = 0; i < History.Count; i++)
            {
                weightReturner = CheckWeight(weightList, History[i].CategoryID, History[i].GenreID);
                if (weightReturner.hasCategory && weightReturner.hasGenre)
                {
                    weightList[weightReturner.ArrayLocation].Weight++;
                }
                else if (weightReturner.hasCategory && History[i].CategoryID == 1 && !weightReturner.hasGenre)
                {
                    RecommendationWeight recommendationWeight = new RecommendationWeight { CategoryID = History[i].CategoryID, Weight = 1, GenreID = History[i].GenreID};
                    weightList.Add(recommendationWeight);

                }
                else if (weightReturner.hasCategory && !weightReturner.hasGenre)
                {
                    weightList[weightReturner.ArrayLocation].Weight++;
                }
                else
                {
                    RecommendationWeight recommendationWeight = new RecommendationWeight { CategoryID = History[i].CategoryID, Weight = 1, GenreID = 0};
                    weightList.Add(recommendationWeight);
                }
            }
            return weightList;
        }

        public WeightReturner CheckWeight(List<RecommendationWeight> weights, int CategoryID, int GenreID)
        {
            WeightReturner weightReturner = new WeightReturner();
            for(int i = 0; i<weights.Count; i++)
            {
                if(CategoryID == 1 && weights[i].CategoryID == CategoryID)
                {
                    weightReturner.hasCategory = true;
                    if(GenreID != 0)
                    {
                        weightReturner.hasGenre = true;
                    }
                    else
                    {
                        weightReturner.hasGenre = false;
                    }
                    weightReturner.ArrayLocation = i;
                    return weightReturner;
                }
                else if(weights[i].CategoryID == CategoryID)
                {
                    weightReturner.hasCategory = true;
                    weightReturner.hasGenre = false;
                    weightReturner.ArrayLocation = i;
                    return weightReturner;
                }
            }
            weightReturner.hasCategory = false;
            return weightReturner;
        }
    }


}
