using System;
using System.Collections.Generic;
using RESTAPI.Models;
using System.Linq;

namespace RESTAPI.Facade
{
    public class BusFacCore
    {
        private bool _hasError = false;
        private string _errorMessage = null;
        private string _errorStacktrace = null;

        private readonly AppSettings _settings;
        public bool HasError
        {
            get { return _hasError; }
        }
        public string ErrorStacktrace
        {
            get { return _errorStacktrace; }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
        }
        public BusFacCore(AppSettings settings)
        {
            _settings = settings;
        }
        public List<Apikey> ApikeyGetList()
        {
            List<Apikey> items = null;
            try
            {
                using (var db = new ApikeyContext(_settings.DefaultConnection))
                {
                    IQueryable<Apikey> q;
                    q = db.Apikeys;
                    items = q.ToList();
                    //foreach (var k in db.Apikeys)
                    //{

                    //}
                }
            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return items;
        }
    }
}
