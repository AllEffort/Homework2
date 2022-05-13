# Homework2
Vezbanje rada sa treeview i fajlovima

1)Objasnjenje formatiranja u "bazi"
  * Tekstualni fajl je organizovan po principu da ukoliko ne pocinje sa praznim poljem onda je dete prvog nivoa i dodaje se direktno u treeview, a ukolilko pocinje onda je dete drugog i dodaje se kao ucenik poslednjeg registrovanog smera. Ukoliko budemo imali vise dece, recimo svaki smer ima podsmer onda bismo brojali prazna polja. Postoje i durgi nacini ali ovaj mi se svideo jer se tekst u datoteci nalazi po vizuelnom poretku kakav je i u programu.

2) Uvidi i ideje
  * NE UNOSITI DIREKTNO U TEKSTUALNU DATOTEKU UKOLIKO NISTE UPOZNATI SA PRAVILIMA FORMATIRANJA. Nije odradjen "error handling za taj deo", odraditi u buducnosti, ideja za github zajednicki odeljenski projekat, debug
  * Bolje imenovanje promenljivih i konstanti, klasa DUNP-a trebalo bi da bude nesto pribliznije kao "Stanje univerziteta", dok bi DUNP mogao da bude objekat kao univerzitet, moze da dovede do zabune, citljivost
  * Ponavljanje funkcija prilikom rada na istom mestu, umesto stavljanja vrednosti koje daje u jednu promenljivu i rad sa njom, optimizacija
  * Koriscenje rekurzivne funkcije za unos i citanje sa tekstualnog fajla koja broji prazna polja i registruje poredak (pr: Count(int Broj_Praznih_Polja), omogucila bi bolje "scale"-anje projekta, odnosno ukoliko ucenici nisu poslednji u anarhiji, nego recimo svaki ucenik ima svoj "index" objekat ili sve univerzitete stavimo u celinu "Fakulteti u X zemlji", scaling
  * Definisanje krupnih poslova u posebnim funkcijama i podele na manje radne jedinice koji bi mogli drugde da se koriste, poboljsanje citljivosti itd...
  * Pored cistog istrazivanja objekata sa kojim radimo istraziti i koje objekte nasledjuje radi moguceg poznavanja metoda nekih od istih (pr: IENUMERABLE->ViewTree.Items, moguce koriscenje LINQ zbog nasledjivanja od IENUMERABLE), citljivost i efikasniji rad
 
3) Pitanja
  * Razlike izmedju TreeView.SelectedItem, SelectedValue, SelectedValuePath?
  * Kako registrovati na kojoj se dubini nalazi treeview idem?
  * Kako koristiti literaturu, pre svega widnows dokumentaciju za C#, dosta stvari cini mi se da preskacemo zato moze da dodje do zabune prilikom stvaranja problema koji se tice tih stvari?
  * Da li se prilikom rada preopterecujem enkapsulacijom i koliko je zdrava granica?
  * Prilikom rada sa eventovima, da li "subskrajbanje" na event treba da se vrsi u objektu koji se subskrajba ili u objektu u kojem je event ili je u redu u oba slucaja ukoliko prilika dozvoljava, (pr:U klasi DUNP ubaciti f subToEvent() koja bi bila pokrenuta u constructoru Ucenika ili nekoj njegovoj funkciji radi povezivanja=?


Aksović Dejan, The coding magician 2022
