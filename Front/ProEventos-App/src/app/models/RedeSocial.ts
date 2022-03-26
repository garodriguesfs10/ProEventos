import { Evento } from "./Evento";
import { Palestrante } from "./Palestrante";

export interface RedeSocial {
 id : number;
 nome : string;
 URL : string;
 eventoId? : number;
 //Evento : Evento;
 palestranteId? : number;
// Palestrante : Palestrante;
}
