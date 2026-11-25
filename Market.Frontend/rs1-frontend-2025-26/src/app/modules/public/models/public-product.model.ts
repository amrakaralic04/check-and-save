export interface PublicProduct {
  id: number;
  name: string;
  description: string;
  price: number;
  brand: string;
  rating: number;
  imageUrl: string;
  images: string[];
  category: string;
}

export const PUBLIC_PRODUCTS: PublicProduct[] = [
  {
    id: 1,
    name: 'Apple iPhone 15',
    description: '6.1" Super Retina XDR, A16 Bionic, 128GB memorije.',
    price: 1699,
    brand: 'Apple',
    rating: 4.8,
    imageUrl: 'https://images.unsplash.com/photo-1696446702183-8f5d6f8baf9d?auto=format&fit=crop&w=1200&q=80',
    images: [
      'https://images.unsplash.com/photo-1696446702183-8f5d6f8baf9d?auto=format&fit=crop&w=1200&q=80',
      'https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?auto=format&fit=crop&w=1200&q=80',
      'https://images.unsplash.com/photo-1580910051074-3eb694886505?auto=format&fit=crop&w=1200&q=80',
    ],
    category: 'Smartphone'
  },
  {
    id: 2,
    name: 'Samsung Galaxy S24',
    description: 'Dynamic AMOLED 2X, 120Hz, 256GB memorije.',
    price: 1599,
    brand: 'Samsung',
    rating: 4.7,
    imageUrl: 'https://images.unsplash.com/photo-1610945265064-0e34e5519bbf?auto=format&fit=crop&w=1200&q=80',
    images: [
      'https://images.unsplash.com/photo-1610945265064-0e34e5519bbf?auto=format&fit=crop&w=1200&q=80',
      'https://images.unsplash.com/photo-1565849904461-04a58ad377e0?auto=format&fit=crop&w=1200&q=80',
      'https://images.unsplash.com/photo-1512054502232-10a0a035d672?auto=format&fit=crop&w=1200&q=80',
    ],
    category: 'Smartphone'
  },
  {
    id: 3,
    name: 'Sony WH-1000XM5',
    description: 'Premium noise-cancelling bluetooth slušalice.',
    price: 799,
    brand: 'Sony',
    rating: 4.9,
    imageUrl: 'https://images.unsplash.com/photo-1546435770-a3e426bf472b?auto=format&fit=crop&w=1200&q=80',
    images: [
      'https://images.unsplash.com/photo-1546435770-a3e426bf472b?auto=format&fit=crop&w=1200&q=80',
      'https://images.unsplash.com/photo-1505740420928-5e560c06d30e?auto=format&fit=crop&w=1200&q=80',
      'https://images.unsplash.com/photo-1484704849700-f032a568e944?auto=format&fit=crop&w=1200&q=80',
    ],
    category: 'Audio'
  },
  {
    id: 4,
    name: 'Dell XPS 13',
    description: 'Ultra lagan laptop sa Intel Core i7 procesorom.',
    price: 2499,
    brand: 'Dell',
    rating: 4.6,
    imageUrl: 'https://images.unsplash.com/photo-1611186871348-b1ce696e52c9?auto=format&fit=crop&w=1200&q=80',
    images: [
      'https://images.unsplash.com/photo-1611186871348-b1ce696e52c9?auto=format&fit=crop&w=1200&q=80',
      'https://images.unsplash.com/photo-1496181133206-80ce9b88a853?auto=format&fit=crop&w=1200&q=80',
      'https://images.unsplash.com/photo-1593642632559-0c6d3fc62b89?auto=format&fit=crop&w=1200&q=80',
    ],
    category: 'Laptop'
  }
];

export function findPublicProductById(id: number): PublicProduct | null {
  return PUBLIC_PRODUCTS.find(product => product.id === id) ?? null;
}
