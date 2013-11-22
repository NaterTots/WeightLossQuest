using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ServiceManager
{
    private static ServiceManager _instance;
    public static ServiceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ServiceManager();
            }

            return _instance;
        }
    }

    private Dictionary<ServiceType, IService> _services = new Dictionary<ServiceType,IService>();

    private ServiceManager()
    {

    }

	public void AddService(ServiceType st, IService service)
	{
		_services.Add(st, service);
	}

    public IService GetService(ServiceType type)
    {
        return _services[type];
    }

    public T GetService<T>(ServiceType type) where T : IService
    {
        return (T)_services[type];
    }
}

public interface IService
{
    ServiceType GetServiceType();
}

