import { Component, inject } from '@angular/core';
import { LanguageService } from '../../../../core/services/language.service';

interface Skill {
  name: string;
  icon: string;
}

@Component({
  selector: 'app-skills',
  standalone: true,
  templateUrl: './skills.component.html'
})
export class SkillsComponent {
  protected readonly t = inject(LanguageService).t;

  protected readonly programming: Skill[] = [
    { name: 'MySQL', icon: 'devicon-mysql-original colored' },
    { name: 'Angular', icon: 'devicon-angularjs-plain colored' },
    { name: '.NET', icon: 'devicon-dot-net-plain-wordmark colored' },
    { name: 'Node.js', icon: 'devicon-nodejs-plain colored' },
    { name: 'JavaScript', icon: 'devicon-javascript-plain colored' },
    { name: 'React', icon: 'devicon-react-original colored' },
    { name: 'JSON', icon: 'devicon-json-plain colored' },
    { name: 'Java', icon: 'devicon-java-plain colored' },
    { name: 'PHP', icon: 'devicon-php-plain colored' },
    { name: 'TypeScript', icon: 'devicon-typescript-plain colored' },
    { name: 'HTML', icon: 'devicon-html5-plain colored' },
    { name: 'CSS', icon: 'devicon-css3-plain colored' },
  ];

  protected readonly tools: Skill[] = [
    { name: 'UI/UX', icon: 'fas fa-object-group fa-gradient' },
    { name: 'Git', icon: 'devicon-git-plain colored' },
    { name: 'Docker', icon: 'devicon-docker-plain colored' },
    { name: 'DevOps', icon: 'fas fa-infinity fa-gradient' },
  ];
}
