// See https://aka.ms/new-console-template for more information


using TimetableMaker;

Algorithm algorithm = new Algorithm();
algorithm.Start();


//!trebuie sa modific cromozomul. Momentan produsul zile*ore*sali este prea mic pentru mai mult de 2 grupe, pentru 3 grupe, 2 zile, 2 sali, 5 ore
//as avea nevoie de  un cromozom cu lungimea de 30, dar la mine e maxim 20. Aici ar trebui sa am cel mult 4 din cele 6 ore ocupate per grupa
//adica presupun ca un curs de 2 ore are duratia 1, deci pentru duratie 4 ar fi 8 ore. Astfel per zi, per grupa, ar trebui sa iau in vedere sa se ajunga 
//la 8 ore ocupate (deci duratie 4 in orar). 
//Also feature sa fac in asa fel incat profesorii sa aiba cat mai putine pauze in program, si sa aiba clar un maxim de 4 ore per zi.
//!trebuie sa modific functia de fitness. Poate sa dea 100% chiar daca e doar 40%
//!trebuie sa modific ori mutatia ori crossover-ul. Poate sa imi dea sa am un cromozom cu doar 2 pozitii ocupate din lungime 