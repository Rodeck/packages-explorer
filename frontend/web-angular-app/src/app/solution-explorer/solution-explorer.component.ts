import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { Project, Solution } from 'src/models/solution-model';
import { SolutionsService } from '../solutions-service.service';

@Component({
  selector: 'app-solution-explorer',
  templateUrl: './solution-explorer.component.html',
  styleUrls: ['./solution-explorer.component.css']
})
export class SolutionExplorerComponent implements OnInit {

  public packageName: string;
  public defaultProjectsDisplay = 4;
  public solutionDisplay = [];

  public solutions: Solution[];

  constructor(private service: SolutionsService) { }

  ngOnInit(): void {
  }

  public expandProjects(solutionIndex: number) : void
  {
    let project = solutionIndex == null ? null : solutionIndex.toString()
    console.log(project);
    this.solutionDisplay[project] = true;
  }

  public collapseProjects(solutionIndex: number) : void
  {
    let project = solutionIndex == null ? null : solutionIndex.toString()
    this.solutionDisplay[project] = false;
  }

  public isExpanded(solutionIndex: number): boolean
  {
    let project = solutionIndex == null ? null : solutionIndex.toString()
    return this.solutionDisplay[project];
  }

  public showProjectsCount(array: Project[], solutionIndex: number) : Project[]
  {
    let project = solutionIndex == null ? null : solutionIndex.toString()
    let expanded = this.solutionDisplay[project];

    return expanded === true ? array : array.slice(0, this.defaultProjectsDisplay);
  }

  public getHowManyMore(array: Project[], solutionIndex: number) : number
  {
    let project = solutionIndex == null ? null : solutionIndex.toString()
    let expanded = this.solutionDisplay[project];

    return expanded == true ? 0 : array.length - this.defaultProjectsDisplay;
  }

  public search()
  {
    var text = this.packageName;

    this.service.GetSolutions(text)
      .subscribe(solutions => 
        {
          this.solutions = solutions;

          this.solutions.forEach((s, idx) => {
            this.solutionDisplay[idx.toString()] = false;
          })
        });
  }

}
