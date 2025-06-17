export interface Professor {
  id: number;
  name: string;
  collegeId: number; // aici o sa iau numele facultatii?? idk
}

export type CreateProfessor = {
  name: string;
  collegeId: number;
};