import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  public eventos: any = [];
  public eventosFiltrados: any = [];

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

  filtrarEventos(filtrarPor: string): any
  {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter
      (
        (evento: { tema: string; local: string; }) =>
          evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
          evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
      );
  }
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    // método que é chamado antes do html ser formado
    this.getEventos();
  }

  public getEventos(): void {

    this.http.get('https://localhost:5001/api/eventos').subscribe(
      response => {
        this.eventos = response;
        this.eventosFiltrados = this.eventos;
      },
      error => console.log(error)
    );
  }

  public metodoMostrarImagem(): void {
    this.mostrarImagem = !this.mostrarImagem;
  }
}