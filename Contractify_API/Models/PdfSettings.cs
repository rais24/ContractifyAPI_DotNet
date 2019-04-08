using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Builders;
using System;

namespace Contractify_API.Models
{
    public class PdfSettings
    {
        private readonly MongoHelper<PdfSettings> _settings;

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SettingId { get; set; }
        public string CompanyId { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyContact { get; set; }
        public string CompanyUrl { get; set; }
        public string CompanyWork { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public PdfSettings()
        {
            _settings = new MongoHelper<PdfSettings>();
        }

        public bool IsSettingsExist(string companyId)
        {
            var query = Query<PdfSettings>.EQ(x => x.CompanyId, companyId);
            var settings = _settings.Collection.FindOne(query);

            if (settings != null)
            {
                return true;
            }

            return false;
        }

        public PdfSettings GetSettings(string companyId)
        {
            PdfSettings settings = new PdfSettings();

            var query = Query<PdfSettings>.EQ(x => x.CompanyId, companyId);
            settings = _settings.Collection.FindOne(query);

            return settings;
        }

        public bool UpdateSettings(PdfSettings settings)
        {
            bool isUpdated = false;
            if (IsSettingsExist(settings.CompanyId)) // if exists update setting
            {
                var query = Query<PdfSettings>.EQ(x => x.CompanyId, settings.CompanyId);

                PdfSettings oldSettings = GetSettings(settings.CompanyId);
                settings.CreatedDate = oldSettings.CreatedDate;
                settings.UpdatedDate = Convert.ToDateTime(System.DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm tt"));

                var replacement = Update<PdfSettings>.Replace(settings);
                var result = _settings.Collection.Update(query, replacement);

                if (result.DocumentsAffected > 0) isUpdated = true;
            }
            else // else insert new setting
            {
                settings.CreatedDate = Convert.ToDateTime(System.DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm tt"));

                var result = _settings.Collection.Save(settings);

                if (result.DocumentsAffected > 0 || result.Ok) isUpdated = true;

            }

            return isUpdated;
        }

        public bool UpdateLogo(PdfSettings settings)
        {
            bool isUpdated = false;
            if (IsSettingsExist(settings.CompanyId)) // if exists update setting
            {
                var query = Query<PdfSettings>.EQ(x => x.CompanyId, settings.CompanyId);

                settings.UpdatedDate = Convert.ToDateTime(System.DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm tt"));

                var replacement = Update<PdfSettings>.Combine(Update<PdfSettings>.Set(x => x.CompanyLogo,settings.CompanyLogo),
                    Update<PdfSettings>.Set(x => x.UpdatedDate,settings.UpdatedDate));

                var result = _settings.Collection.Update(query, replacement);

                if (result.DocumentsAffected > 0) isUpdated = true;
            }
            else // else insert new setting
            {
                settings.CreatedDate = Convert.ToDateTime(System.DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm tt"));

                var result = _settings.Collection.Save(settings);

                if (result.DocumentsAffected > 0 || result.Ok) isUpdated = true;

            }

            return isUpdated;
        }
    }
}