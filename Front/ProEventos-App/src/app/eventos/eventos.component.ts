import { Component, OnInit } from '@angular/core';
import { Evento } from '../models/Evento';
import { EventoService } from '../services/Evento.service';


@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];

  widthImg: number = 100;
  marginImg: number = 2;
  mostrarImagem: boolean = true;

  private _filtroLista: string = '';

  public get filtroListaEvento() : string
  {
    return this._filtroLista;
  }

  public set filtroListaEvento(value: string)
  {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroListaEvento ?
      this.filtrarEventos(this.filtroListaEvento) : this.eventos;
  }

  public filtrarEventos(filtrarPor: string): Evento[]
  {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter
      (
        (evento: { tema: string; local: string; }) =>
          evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
          evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
      );
  }

  constructor(private eventoService : EventoService) { }

  public ngOnInit(): void {
    // método que é chamado antes do html ser formado
    this.getEventos();
  }

  public getEventos(): void {

    this.eventoService.getEventos().subscribe(
      (_eventos : Evento[]) => {
        this.eventos = _eventos;
        this.eventosFiltrados = this.eventos;
      },
      error => console.log(error)
    );
  }

  public metodoMostrarImagem() : void {
    this.mostrarImagem = !this.mostrarImagem;
  }
}
