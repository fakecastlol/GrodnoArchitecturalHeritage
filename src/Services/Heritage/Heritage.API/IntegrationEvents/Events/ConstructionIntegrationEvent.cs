using System;

namespace Heritage.API.IntegrationEvents.Events
{
    public class ConstructionIntegrationEvent
    {
        public DateTime DateTime { get; set; }
        public string Action { get; set; }
        public string ConstructionName { get; set; }
        public Guid ConstructionId { get; set; }

        public ConstructionIntegrationEvent(DateTime dateTime, string action, Guid constructionId)
        {
            DateTime = dateTime;
            Action = action;
            ConstructionId = constructionId;
        }

        public ConstructionIntegrationEvent(DateTime dateTime, string action, string constructionName, Guid constructionId)
        {
            DateTime = dateTime;
            Action = action;
            ConstructionName = constructionName;
            ConstructionId = constructionId;
        }
    }
}