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
		EventManager eventManager = new EventManager();
		_services.Add(ServiceType.EventManager, eventManager);
        _services.Add(ServiceType.InputManager, GameObject.Find ("InputManager").GetComponent<InputManager>());
		_services.Add(ServiceType.StatsManager, new StatsManager(eventManager));
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

public enum ServiceType
{
    EventManager,
    InputManager,
	StatsManager
}

public interface IService
{
    ServiceType GetServiceType();
}

