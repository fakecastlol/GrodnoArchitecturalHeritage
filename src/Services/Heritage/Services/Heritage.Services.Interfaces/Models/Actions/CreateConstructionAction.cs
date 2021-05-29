using System;

namespace Heritage.Services.Interfaces.Models.Actions
{
    public class CreateConstructionAction
    {
        private DateTime dateTime;

        private Guid id;

        private string url;

        public object inputModel;

        public object outputModel;

        public CreateConstructionAction(DateTime dateTime, Guid id, string url)
        {
            this.dateTime = dateTime;
            this.id = id;
            this.url = url;
        }

        public DateTime DateTime
        {
            get => dateTime;
            set => dateTime = value;
        }

        public Guid Id
        {
            get => id;
            set => id = value;
        }

        private string Url
        {
            get => url;
            set => url = value;
        }

        public object InputModel
        {
            get => inputModel;
            set => inputModel = value;
        }

        public object OutputModel
        {
            get => outputModel;
            set => outputModel = value;
        }

    }
}
