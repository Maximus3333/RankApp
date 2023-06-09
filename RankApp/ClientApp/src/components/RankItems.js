import { useEffect, useState } from "react";
import RankingGrid from "./RankingGrid";
import ItemCollection from "./ItemCollection";
import AddItemForm from "./AddItemForm";

const RankItems = ({ items, setItems, dataType, imgArr, localStorageKey }) => {
  const [reload, setReload] = useState(false);

  function Reload() {
    
    setReload(true);
  }

  function drag(ev) {
    ev.dataTransfer.setData("text", ev.target.id);
  }

  function allowDrop(ev) {
    ev.preventDefault();
  }
  const updateItemRanking = (itemId, updatedItem) => {
    // Make the PUT request to update the item ranking
    fetch(`/item/${itemId}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(updatedItem),
    })
      .then(response => response.json())
      .then(updatedItem => {
        // Handle the updated item response
        console.log('Item updated:', updatedItem);
        // Perform any necessary actions after successful update
      })
      .catch(error => {
        // Handle any errors
        console.error('Error updating item:', error);
        // Perform any necessary error handling
      });
  };

  function drop(ev) {
    ev.preventDefault();
    const targetElm = ev.target;
    if (targetElm.nodeName === "IMG") {
      return false;
    }
    if (targetElm.childNodes.length === 0) {
      var data = parseInt(ev.dataTransfer.getData("text").substring(5));
      const transformedCollection = items.map((item) => {
        if (item.id === parseInt(data)) {
          const updatedItem = { ...item, ranking: parseInt(targetElm.id.substring(5)) };
          
          // Call the function to update the item ranking
          updateItemRanking(item.id, updatedItem);
          
          return updatedItem;
        } else {
          return { ...item, ranking: item.ranking };
        }
      });
      
      setItems(transformedCollection);
    }
  }
  useEffect(() => {
    console.log("hello");

    if (items == null) {
      console.log("hello2");

      getDataFromApi();
    }
    console.log(items);
  }, [dataType]);

  function getDataFromApi() {
    console.log("what the heck");
    fetch(`item/${dataType}`)
      .then((results) => {
        return results.json();
      })
      .then((data) => {
        setItems(data);
      });
  }

  const resetRankings = () => {
    return new Promise((resolve, reject) => {
      // Make the API call to reset rankings
      // Assuming you are using fetch or axios for API calls
      fetch('/item/reset-rankings', {
        method: 'POST',
      })
        .then(response => {
          if (response.ok) {
            resolve(); // Resolve the Promise if reset rankings is successful
          } else {
            reject(new Error('Failed to reset rankings')); // Reject the Promise if there's an error
          }
        })
        .catch(error => {
          reject(error); // Reject the Promise if there's an error
        });
    });
  };

  useEffect(() => {
    console.log(reload);
    if (reload === true) {
        resetRankings()
          .then(() => {
            getDataFromApi();
          })
          .catch(error => {
            console.error('Error resetting rankings:', error);
          });
      }
    setReload(false)
  }, [reload]);
  const [showForm, setShowForm] = useState(false);

  const handleAddButtonClick = () => {
    showForm ? setShowForm(false) : setShowForm(true);
  };


  const addItemToDatabase = (item) => {
    // Make an API request to post the item data
    fetch('/item', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(item),
    })
      .then((response) => {
        if (response.ok) {
          // Item successfully added to the database
          console.log('Item added successfully');
          // You can perform additional actions after successful addition if needed
        } else {
          // Error occurred while adding the item
          console.log('Error adding item:', response.status, response);
          // You can handle the error scenario here
        }
      })
      .catch((error) => {
        console.error('Error adding item:', error);
        // Handle any network or other errors
      });
  };
  
  const handleAddItem = (item) => {
    // Handle the logic to add the item to the database
    console.log(item);
    setItems([...items, item]);
  
    // Call the function to add the item to the database
    addItemToDatabase(item);
  
    // Reset the form state
    setShowForm(false);
  };

  return items != null ? (
    <main>
      <button class="addbtn" role="button" onClick={handleAddButtonClick}>
        Add {dataType}
      </button>
      {showForm ? <AddItemForm onAddItem={handleAddItem} items={items} /> : null}

      <RankingGrid
        items={items}
        drag={drag}
        allowDrop={allowDrop}
        drop={drop}
      />
      <ItemCollection items={items} drag={drag} />
      <button onClick={Reload} className="reload" style={{ marginTop: "10px" }}>
        {" "}
        <span className="text">Reload</span>{" "}
      </button>
    </main>
  ) : (
    <main>Loading...</main>
  );
};
export default RankItems;
