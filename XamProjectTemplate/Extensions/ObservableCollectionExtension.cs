using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace XamProjectTemplate.Extension
{
    public static class ObservableCollectionExtension
    {
        public static void UnselectAllItems<T>(this ObservableCollection<T> list) where T : ISelect
        {
            if (list.ToList().Exists(x => x.IsSelected))
            {
                list.FirstOrDefault(x => x.IsSelected).IsSelected = false;
            }
        }

        public static void UnselectItemById<T>(this ObservableCollection<T> list, int id) where T : ISelect
        {
            if (list.ToList().Exists(x => x.Id == id))
            {
                list.FirstOrDefault(x => x.Id == id).IsSelected = false;
            }
        }

        public static void SelectItemById<T>(this ObservableCollection<T> list, int id) where T : ISelect
        {
            if (list.ToList().Exists(x => x.Id == id))
            {
                list.FirstOrDefault(x => x.Id == id).IsSelected = true;
            }
        }

        public static void ClearDefault<T>(this ObservableCollection<T> list) where T : IDefault
        {
            if (list.ToList().Exists(x => x.IsDefault))
            {
                list.FirstOrDefault(x => x.IsDefault).IsDefault = false;
            }
        }

        public static void SetDefault<T>(this ObservableCollection<T> list, int id) where T : IDefault
        {
            if (list.ToList().Exists(x => x.Id == id))
            {
                list.FirstOrDefault(x => x.Id == id).IsDefault = true;
            }
        }

        public static void InsertItem<T>(this ObservableCollection<T> list,int index, T item) where T : IAddItem
        {
            if (list.ToList().Exists(x => x.Id == item.Id) )
            {
                App.Log("Updating Item!");
                list.FirstOrDefault(x => x.Id == item.Id).Update(item);
            }
            else
            {
                App.Log("Inserting Item!");
                list.Insert(0,item);
            }
        }

        public static void AddItem<T>(this ObservableCollection<T> list, T item) where T : IAddItem
        {
            if (list.ToList().Exists(x => x.Id == item.Id))
            {
                App.Log("Updating Item!");
                list.FirstOrDefault(x => x.Id == item.Id).Update(item);
            }
            else
            {
                App.Log("Adding Item!");
                list.Add(item);
            }
        }

        public static void UpdateItem<T>(this ObservableCollection<T> list, T item) where T: IAddItem
        {
            if (list.ToList().Exists(x => x.Id == item.Id))
            {
                App.Log("Updating Item!");
                list.FirstOrDefault(x => x.Id == item.Id).Update(item);
                return;
            }
            App.Log("Updating Item Error!, item not found");
        }

        public static void RemoveItem<T>(this ObservableCollection<T> list, T item) where T : IAddItem
        {
            int index = list.ToList().FindIndex(x => x.Id == item.Id);
            if (index != -1)
            {
                App.Log("Removing Item!");
                list.RemoveAt(index);
                return;
            }
            App.Log("Removing Item Error!, item not found");
        }
    }
}
