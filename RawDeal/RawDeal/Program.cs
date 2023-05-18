using RawDeal;
using RawDealView;

// string folder = "06-BasicHybridCards";
string folder = "04-NoEffects";
// string folder = "08-Reversals";
int idTest = 3;
string pathToTest = Path.Combine("data", $"{folder}-Tests", $"{idTest}.txt");

// Esta vista permite verificar el comportamiento de un test particular.
// Intenté que el texto en consola salga azúl si el output es el esperado y rojo si no lo es
//                                  ... pero no testié suficiente este nuevo feature así que no prometo nada :P
// View view = View.BuildManualTestingView(pathToTest); 

// También puedes usar la vista antigua si quieres.
View view = View.BuildConsoleView();  

string deckFolder = Path.Combine("data", folder);
// Formatter formatter = new Formatter(view);  
Game game = new Game(view, deckFolder);
game.Play();
