import React, { useState } from 'react';

const AddItemForm = ({ onAddItem, items }) => {
  const [title, setTitle] = useState('');
  const [imageUrl, setImageUrl] = useState('');
  const [itemType, setItemType] = useState('');

  const handleTitleChange = (event) => {
    setTitle(event.target.value);
  };

  const handleImageUrlChange = (event) => {
    setImageUrl(event.target.value);
  };


  const handleItemTypeChange = (event) => {
    setItemType(event.target.value);
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    if (title && imageUrl && itemType) {
    const id = items.length + 1;

      onAddItem({ id : id , title : title, imageUrl: imageUrl, itemType : itemType, ranking: 0 });
      setTitle('');
      setImageUrl('');
      setItemType('');
    }
  };

  const isSubmitDisabled = !(title && imageUrl && itemType);

  return (
    <form onSubmit={handleSubmit}>
      <label>
        Title:
        <input type="text" value={title} onChange={handleTitleChange} />
      </label>
      <label>
        Image URL:
        <input type="text" value={imageUrl} onChange={handleImageUrlChange} />
      </label>
      <label>
        Item Type:
        <select value={itemType} onChange={handleItemTypeChange}>
          <option value="">Select an option</option>
          <option value="movie">movie</option>
          <option value="album">album</option>
        </select>
      </label>
      <button type="submit" disabled={isSubmitDisabled}>Add</button>
    </form>
  );
};

export default AddItemForm;
