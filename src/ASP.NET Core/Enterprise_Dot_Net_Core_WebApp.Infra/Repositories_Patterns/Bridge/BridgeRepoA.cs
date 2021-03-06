using Enterprise_Dot_Net_Core_WebApp.Core.Entities;
using Enterprise_Dot_Net_Core_WebApp.Core.Interface;
using Enterprise_Dot_Net_Core_WebApp.Core.Interface.DesignPatterns.Bridge;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Enterprise_Dot_Net_Core_WebApp.Infra.Repositories_Patterns.Bridge
{
    public class BridgeRepoA : IBridge, IDisposable
    {
        private readonly IGenericTypeRepository<Enterprise_MVC_Core> repo;
        private IList<Enterprise_MVC_Core_Variant> variant;

        public BridgeRepoA(IGenericTypeRepository<Enterprise_MVC_Core> _repo)
        {
            this.repo = _repo;
        }

        public Task<object> GetAll()
        {
            try
            {
                dynamic temp = this.repo.GetAll().Result;

                if (temp != null || temp != "")
                {
                    variant = new List<Enterprise_MVC_Core_Variant>();

                    for (int i = 0; i < temp.Count; i++)
                    {
                        variant.Add(new Enterprise_MVC_Core_Variant()
                        {
                            ID = temp[i].ID,
                            Name = temp[i].Name,
                            Age = temp[i].Age,
                            Commit = "From Bridge repository A."
                        });
                    }
                }
                return Task.Run(() => variant as object);
            }
            catch (Exception ex)
            {
                return Task.FromResult((object)null);
            }
            finally
            {
                Dispose();
            }
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
