import { PlayerService } from './../../services/player/player.service';
import { Player } from './../../models/player/player';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss']
})
export class ModalComponent implements OnInit {
  playerForm: FormGroup;
  submitted = false;
  clicked = false;
  @Input()
  showModal: boolean;
  constructor(private formBuilder: FormBuilder, private playerService: PlayerService) { }
  message: String;
  player: Player;
  @Output() playerEvent = new EventEmitter<Player>();
  @Output() messageEvent = new EventEmitter<String>();

  ngOnInit() {
    this.clicked = false;
    this.playerForm = this.formBuilder.group({
      firstName: ['', Validators.required]
    });
  }

  get f() { return this.playerForm.controls; }

  onSubmit() {
    this.clicked = true;
    if (this.playerForm.invalid) {
      return;
    }
    let player: Player = new Player();
    player.firstName = this.f.firstName.value;
    let params = new HttpParams()
      .set('$filter', `FirstName  eq '${player.firstName}'`);

    this.playerService.list(params).subscribe(response => {
      if (response.length == 0) {
        this.playerService.create(player).subscribe(response => {
          this.player = response;
          this.message = `Welcome ${response.firstName} !`;
          this.playerEvent.emit(this.player);
          this.messageEvent.emit(this.message);
        });
      } else {
        this.player = response[0];
        this.message = `Welcome back ${this.player.firstName} !`;  
        this.messageEvent.emit(this.message);
        this.playerEvent.emit(this.player);
      }
      this.submitted = true;

    })
  }
}
