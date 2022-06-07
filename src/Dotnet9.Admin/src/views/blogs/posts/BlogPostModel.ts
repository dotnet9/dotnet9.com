import internal from "stream";

interface BlogPostWithDetails {
  id: number;
  title: string;
  slug: string;
  briefDescription: string;
  content: string;
  cover: string;
  copyrightType: number,
  original: string;
  originalTitle: string;
  originalLink: string;
  inBanner: boolean;
  updateTime: string;
  albumNames: Album[];
  categoryNames: Category[];
  tags: string[];
  createDate: string;
}

interface Category {
  slug: string;
  name: string;
}

interface Album {
  slug: string;
  name: string;
}

export {
    BlogPostWithDetails
}