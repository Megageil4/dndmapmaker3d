using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class CurrentUserController : MonoBehaviour
    {
        [SerializeField] private GameObject currentUserFieldPrefab;
        [SerializeField] private Transform currentUserFieldParent;

        public void UpdateUsers(List<string> users)
        {
            RemoveAllUsers();
            foreach (var user in users)
            {
                AddUser(user);
            }
        }
        
        private void AddUser(string username)
        {
            var currentUserField = Instantiate(currentUserFieldPrefab, currentUserFieldParent);
            currentUserField.GetComponent<CurrentUserField>().SetUsername(username);
        }

        private void RemoveAllUsers()
        {
            foreach (Transform child in currentUserFieldParent)
            {
                Destroy(child.gameObject);
            }
        }
    }
}