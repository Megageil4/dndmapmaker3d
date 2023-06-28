using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    /// <summary>
    /// Controller for the UI that shows the list of current users
    /// </summary>
    public class CurrentUserController : MonoBehaviour
    {
        /// <summary>
        /// The prefab for a single user field that will be added to the UI for each user
        /// </summary>
        [SerializeField] private GameObject currentUserFieldPrefab;
        /// <summary>
        /// The container for the currentUserFieldPrefabs. This is where the user fields will be added
        /// </summary>
        [SerializeField] private Transform currentUserFieldParent;

        /// <summary>
        /// Takes a list of users and updates the UI to show them.
        /// It will remove all users that are currently shown.
        /// </summary>
        /// <param name="users">The list of users to show</param>
        public void UpdateUsers(List<string> users)
        {
            RemoveAllUsers();
            foreach (var user in users)
            {
                AddUser(user);
            }
        }
        
        // Adds a single user to the UI
        private void AddUser(string username)
        {
            var currentUserField = Instantiate(currentUserFieldPrefab, currentUserFieldParent);
            currentUserField.GetComponent<CurrentUserField>().SetUsername(username);
        }
        
        // Removes all users from the UI
        private void RemoveAllUsers()
        {
            foreach (Transform child in currentUserFieldParent)
            {
                Destroy(child.gameObject);
            }
        }
    }
}