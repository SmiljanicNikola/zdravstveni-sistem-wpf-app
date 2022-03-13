<b>Specifikacija Projekta</b><br><br>
Aplikacija ima sledece entitete:
1. <b>Adresa</b> - sifra(jedinstvena), ulica, broj, grad, drzava
2. <b>Dom zdravlja</b> = sifra(jedinstvena), naziv institucije i Adresa u kom se nalazi
3. <b>Registrovan korisnik</b> - ime, prezime, JMBG(jedinstveno), email, Adresa, pol, lozinka, tip Registrovanog korisnika(Registrovani korisnici aplikacije su admini, lekari i pacijenti)
4. <b>Lekar</b> - Registrovan korisnik (JMBG), Dom Zdravlja u kojem je lekar zaposlen i lista Termina(Zakazanih i Slobodnih)
5. <b>Pacijent</b> - Registorvan korisnik (JMBG), lista zakazanih Termina i lista Terapija
6. <b>Terapija</b> - sifra(jedinstvena), opis terapije, Lekar koji je prepisao Terapiju
7. <b>Termin</b> - sifra(jedinstvena), Lekar kod kojeg je termin zakazan, datum termina, status termina(Slobodan ili zakazan) i Pacijent koji je zakazao termin.
Napomena: Termin postoji iako je status SLOBODAN

Aplikaciju mogu da koriste i registrovani i neregistrovani korisnici. Registrovani korisnici su administratori, lekari i pacijenti.
Administratori su predefinisani u bazi podataka.

<b>Funkcionalnosti aplikacije:</b>

<b>1.</b> Neprijavljen tj neregistrovan korisnik ima pregled svih domova zdravlja na osnovu odabranog mesta i lekara koji rade u odabranoj instituciji. Nema mogucnost zakazivanja termina.

<b>2.</b> Omoguciti korisnicima da se prijave i odjave sa sistema. Prilikom prijave na sistem korisnik unosi <b>JMBG</b> i <b>Lozinku</b> sa kojom se registrovao.

<b>3.</b> Svi registrovani korisnici imaju pregled svojih licnih podataka i mogucnost izmene istih. Pacijent se sam registruje na sistem. Prilikom registracije pacijentu se kreira prazan zdravstveni karton. Zdravstveni karton podrazumeva listu terapija koje je pacijent do sada imao(Ne treba da se kreira nova klasa Zdravstveni Karton).

<b>4.</b> Administrator ima pregled svih entiteta u aplikaciji. Moze sve podatke da doda, izmeni i obrise. Sva brisanja su logicka(Ne uklanjaju se fizicki tj podataka postaje neaktivan i vise se ne prikazuje, niti se moze obrisati i izmeniti). Administratori kreiraju inicijalno SLOBODNE termine lekarima. Administrator moze da brise termine bez obzira da li su zakazani ili slobodni. Samo administratori mogu da kreiraju lekare.

<b>5.</b> Lekar ima mogucnost pregleda svih domova zdravlja u odabranom mestu i prikaz svojih termina(Slobodnih i zauzetih) u odabranoj instituciji u kojoj je zaposlen. Lekari imaju pregled zdravstvenih kartona pacijenata koji su kod njega zakazali pregled. Lekari mogu da dodaju terapiju svom pacijentu.
  
<b>6.</b> Pacijent ima mogucnost pregleda svih domova zdravlja u odabranom mestu, lekara u odabranim institucijama. Pacijent ima samo pregled svog zdravstvenog kartona koji ne moze da
brise, menja ili dodaje. Takodje ima pregled slobodnih i zauzetih termina svih lekara i mogucnost da zakaze slobodan termin za pregled.

<b>7.</b> Pretraga entiteta je omogucena gde korisnici (registrovani i neregistrovani) imaju pregled sledecih entiteta:<br>
   <b>a. Registrovani korisnici</b> - po imenu, prezimenu, email, adresa i po tipu registrovanih korisnika<br>
   <b>b. Domovi zdravlja</b> - po nazivu instituta, adresi(Po ulici i/ili broju)<br>
   <b>c. Terapije</b> - po opisu i lekaru<br>
   
Omoguciti prikaz svih termina(zauzetih i slobodnih) za odabranog lekara.

Omoguciti da lekari kreiraju i brisu svoje slobodne termine. Ogranicenje prilikom brisanja termina je to da lekar ne moze da obrise termin ako je vec ZAKAZAN.

Perzistenciju podataka realizovati koriscenjem relacione baze podataka.

<b>SQL za kreiranje svih potrebnih tabela u okviru baze podatak nalazi se u fajlu data.sql</b>
