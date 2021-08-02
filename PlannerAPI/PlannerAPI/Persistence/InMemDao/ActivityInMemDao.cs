using System;
using System.Linq;
using PlannerAPI.Models;
using System.Collections.Generic;
using PlannerAPI.Models.Domain;

namespace PlannerAPI.Persistence
{
    public class ActivityInMemDao : IActivityDao
    {

        List<Activity> _activityList = new List<Activity>();

        public ActivityInMemDao()
        { 
            _activityList.Add(new Activity(1, "Bowling", 50));
            _activityList.Add(new Activity(2, "Running", 20));
            _activityList.Add(new Activity(3, "Jogging", 40));
            _activityList.Add(new Activity(4, "Lifting", 90));
            _activityList.Add(new Activity(5, "Swimming", 50));
        }

        public List<Activity> GetAllActivities()
        {
            return _activityList.Where(a => a != null).ToList();
        }

        public Activity GetActivityById(int id)
        {
            List<Activity> activity = GetAllActivities();

            Activity toReturn = activity.Where(e => e.Id == id).SingleOrDefault();

            if (toReturn == null)
                throw new NullReferenceException("Activity does not exist");

            return toReturn;
        }

        public void RemoveActivity(Activity toRemove)
        {
            _activityList = _activityList.Where(a => a.Id != toRemove.Id).ToList();
        }

        public int AddActivity(Activity toAdd)
        {
            if (toAdd != null)
            {
                _activityList.Add(toAdd);
                return toAdd.Id;
            }
            else
                throw new NullReferenceException("Activity is null");
        }

        public void EditActivity(Activity update)
        {
            List<Activity> activities = GetAllActivities();

            for (int i = 0; i < activities.Count; i++)
            {
                Activity activity = activities[i];
                if (activity.Id == update.Id)
                {
                    activity.Duration = update.Duration;
                    activity.Id = update.Id;
                    activity.Name = update.Name;
                    _activityList[i] = activity;
                }
            }
        }
    }
}
