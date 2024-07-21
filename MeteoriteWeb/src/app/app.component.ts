import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MeteoriteService } from './services/meteorite.service';
import { MeteoriteGroup } from './interfaces/meteorite-group';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { MeteoriteFilter, SortBy, SortOrder, YearFilter } from './interfaces/meteorite-filter';
import { MatSelect } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatTableModule, MatSortModule, MatFormFieldModule, MatSelectModule, MatInputModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('dateFrom') dateFrom: MatSelect;
  @ViewChild('dateTo') dateTo: MatSelect;
  @ViewChild('recClass') recClass: MatSelect;
  @Input() nameSearch: string;

  public displayedColumns = ['year', 'count', 'sumMass'];
  public dataSource = new MatTableDataSource<MeteoriteGroup>([]);

  public dateRange: number[] = [];
  public recClassList: string[] = [];

  public meteoriteGroupTotalCount?: number;
  public meteoriteGroupTotalSumMass?: number;

  private minDate: number;
  private maxDate: number;


  constructor(private _meteoriteService: MeteoriteService) { }

  public ngOnInit(): void {
    this.loadDateRangeFilters();
    this.loadRecClassList();
  }

  loadDateRangeFilters(): void {
    this._meteoriteService.getDateRangeList().subscribe(_ => {
      this.dateRange = _;

      this.minDate = Math.min(..._);
      this.maxDate = Math.max(..._);

      this.loadInitailData();
    });
  }

  loadRecClassList(): void {
    this._meteoriteService.getRecClassList().subscribe(_ => this.recClassList = _);
  }

  loadInitailData(): void {
    let filter: MeteoriteFilter = {
      year: {
        startDateYear: this.minDate,
        endDateYear: this.maxDate
      } as YearFilter,
      sorting: {
        sortBy: SortBy.Year,
        sortOrder: SortOrder.Descending
      }
    }

    this.loadMeteoriteGroups(filter);
  }

  loadMeteoriteGroups(filter: MeteoriteFilter): void {
    this._meteoriteService.getByFilter(filter).subscribe(_ => {
      this.dataSource.data = _;
      this.dataSource.sort = this.sort;

      this.meteoriteGroupTotalCount = this.dataSource.data.length;
      this.meteoriteGroupTotalSumMass = this.meteoriteGroupTotalCount === 0
        ? 0
        : this.dataSource.data
          .map(_ => _.sumMass)
          .reduce((prev, next) => this.getValueOrDefault(prev) + this.getValueOrDefault(next));
    });
  }

  getValueOrDefault(value?: number): number {
    return Number.isFinite(value) ? +value! : 0;
  }

  onDateFilterChange(): void {
    let filter = this.createFilter();

    this.loadMeteoriteGroups(filter);
  }

  onRecClassChange(): void {
    let filter = this.createFilter();

    this.loadMeteoriteGroups(filter);
  }

  onNameChange(): void {
    let filter = this.createFilter();

    this.loadMeteoriteGroups(filter);
  }

  private createFilter(): MeteoriteFilter {
    let filter: MeteoriteFilter = {};

    if (this.dateFrom.value && this.dateTo.value) {
      filter.year = {
        startDateYear: +this.dateFrom.value,
        endDateYear: +this.dateTo.value
      } as YearFilter
    }

    if (this.recClass.value) {
      filter.recClass = this.recClass.value;
    }

    if (this.nameSearch) {
      filter.name = this.nameSearch;
    }

    return filter;
  }
}
