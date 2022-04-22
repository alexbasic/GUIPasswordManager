using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Ru.Mail.AlexBasic.GUIPasswordManager
{
    public class ListViewAdd : ListView
    {
        public ListViewAdd()
        {
        }

        public int GetSelectedSecretId()
        {
            return (SelectedItems.Cast<SecretsListItem>().FirstOrDefault())?.Id ?? -1;
        }

        public void SetValues(IEnumerable<SecretsListItem> secrets, IEnumerable<GroupListView> groups)
        {
            BeginUpdate();
            Items.Clear();
            Groups.Clear();
            foreach (var group in groups)
            {
                Groups.Add(group.ListViewGroup);
            }
            foreach (var secret in secrets)
            {
                var secretGroup = Groups[secret.Name];
                secret.Group = secretGroup;
                Items.Add(secret);
            }

            EndUpdate();
        }

        public class GroupListView
        {
            public int Id { get; private set; }
            public string Name { get; private set; }
            public ListViewGroup ListViewGroup { get; private set; }

            public GroupListView(int id, string name)
            {
                Id = id;
                Name = name;
                ListViewGroup = new ListViewGroup();
                ListViewGroup.Header = name;
                ListViewGroup.Name = name + "ListViewGroup";
                ListViewGroup.HeaderAlignment = HorizontalAlignment.Left;
            }
        }

        public class SecretsListItem : ListViewItem
        {
            public int Id { get; set; }
            public string NameOfSecret { get; set; }
            public string Value { get; set; }
            public string Comment { get; set; }
            public int GroupId { get; set; }
            public string GroupName { get; set; }

            public ListViewGroup ListViewGroup { get; set; }

            public SecretsListItem(int id, string name, string value, string comment, int groupId, string groupName)
            {
                Id = id;
                Value = value;
                Comment = comment;
                GroupId = groupId;
                GroupName = groupName;

                Text = name;
                Name = name + "ListViewItem";
                SubItems.Add(value);
                SubItems.Add(comment);
            }
        }

        public class ListViewGroupSorter : IComparer
        {
            private SortOrder order;

            public ListViewGroupSorter(SortOrder theOrder)
            {
                order = theOrder;
            }

            public int Compare(object x, object y)
            {
                int result = String.Compare(
                    ((ListViewGroup)x).Header,
                    ((ListViewGroup)y).Header
                );
                if (order == SortOrder.Ascending)
                {
                    return result;
                }
                else
                {
                    return -result;
                }
            }
        }
    }
}
