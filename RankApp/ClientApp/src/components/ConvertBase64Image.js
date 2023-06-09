import React from 'react';

const Base64Image = ({ base64String }) => {
  const imageUrl = `data:image/png;base64,${base64String}`;

  return imageUrl
};

export default Base64Image;