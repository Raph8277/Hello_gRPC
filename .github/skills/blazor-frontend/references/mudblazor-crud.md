# MudBlazor CRUD Patterns

## MudDataGrid — List Page

```razor
@page "/personalities"
@inject PersonalityGrpcClient PersonalityClient
@inject IDialogService DialogService
@inject ISnackbar Snackbar

<PageTitle>Personnalités</PageTitle>

<MudText Typo="Typo.h4" Class="mb-4">Personnalités</MudText>

<MudPaper Elevation="2" Class="pa-4">
    <MudStack Row AlignItems="AlignItems.Center" Class="mb-4">
        <MudTextField @bind-Value="_searchTerm"
                      Placeholder="Rechercher..."
                      Adornment="Adornment.Start"
                      AdornmentIcon="@Icons.Material.Filled.Search"
                      Immediate
                      DebounceInterval="300"
                      OnDebounceIntervalElapsed="OnSearch" />
        <MudSelect @bind-Value="_selectedCategory"
                   Label="Catégorie"
                   Clearable
                   Style="width: 200px"
                   ValueChanged="OnCategoryChanged">
            @foreach (var cat in _categories)
            {
                <MudSelectItem Value="@cat">@cat</MudSelectItem>
            }
        </MudSelect>
        <MudSpacer />
        <MudButton Variant="Variant.Filled"
                   Color="Color.Primary"
                   StartIcon="@Icons.Material.Filled.Add"
                   OnClick="OpenCreateDialog">
            Ajouter
        </MudButton>
    </MudStack>

    <MudDataGrid T="PersonalityMessage"
                 ServerData="LoadServerData"
                 @ref="_dataGrid"
                 Hover
                 Dense
                 Striped>
        <Columns>
            <PropertyColumn Property="x => x.FirstName" Title="Prénom" />
            <PropertyColumn Property="x => x.LastName" Title="Nom" />
            <PropertyColumn Property="x => x.Category" Title="Catégorie" />
            <PropertyColumn Property="x => x.Nationality" Title="Nationalité" />
            <PropertyColumn Property="x => x.BirthDate" Title="Naissance" />
            <TemplateColumn Title="Actions" Sortable="false">
                <CellTemplate>
                    <MudStack Row>
                        <MudIconButton Icon="@Icons.Material.Filled.Edit"
                                       Size="Size.Small"
                                       Color="Color.Primary"
                                       OnClick="() => OpenEditDialog(context.Item)" />
                        <MudIconButton Icon="@Icons.Material.Filled.Delete"
                                       Size="Size.Small"
                                       Color="Color.Error"
                                       OnClick="() => OpenDeleteDialog(context.Item)" />
                    </MudStack>
                </CellTemplate>
            </TemplateColumn>
        </Columns>
        <PagerContent>
            <MudDataGridPager T="PersonalityMessage" />
        </PagerContent>
    </MudDataGrid>
</MudPaper>
```

## Code-Behind Pattern

```csharp
@code {
    private MudDataGrid<PersonalityMessage>? _dataGrid;
    private string _searchTerm = "";
    private string? _selectedCategory;
    private List<string> _categories = ["Science", "Art", "Politique", "Sport", "Littérature", "Musique", "Cinéma", "Philosophie", "Histoire", "Technologie"];

    private async Task<GridData<PersonalityMessage>> LoadServerData(GridState<PersonalityMessage> state)
    {
        var response = await PersonalityClient.GetPersonalitiesAsync(
            _searchTerm, _selectedCategory, state.Page * state.PageSize, state.PageSize);

        return new GridData<PersonalityMessage>
        {
            Items = response.Personalities,
            TotalItems = response.TotalCount
        };
    }

    private async Task OnSearch(string _) => await _dataGrid!.ReloadServerData();
    private async Task OnCategoryChanged(string? _) => await _dataGrid!.ReloadServerData();

    private async Task OpenCreateDialog()
    {
        var dialog = await DialogService.ShowAsync<PersonalityFormDialog>("Nouvelle personnalité",
            new DialogParameters(), new DialogOptions { MaxWidth = MaxWidth.Medium, FullWidth = true });
        var result = await dialog.Result;

        if (!result!.Canceled)
        {
            Snackbar.Add("Personnalité créée avec succès.", Severity.Success);
            await _dataGrid!.ReloadServerData();
        }
    }

    private async Task OpenEditDialog(PersonalityMessage personality)
    {
        var parameters = new DialogParameters
        {
            { nameof(PersonalityFormDialog.Personality), personality }
        };
        var dialog = await DialogService.ShowAsync<PersonalityFormDialog>("Modifier la personnalité",
            parameters, new DialogOptions { MaxWidth = MaxWidth.Medium, FullWidth = true });
        var result = await dialog.Result;

        if (!result!.Canceled)
        {
            Snackbar.Add("Personnalité mise à jour avec succès.", Severity.Success);
            await _dataGrid!.ReloadServerData();
        }
    }

    private async Task OpenDeleteDialog(PersonalityMessage personality)
    {
        var parameters = new DialogParameters
        {
            { nameof(ConfirmDeleteDialog.ContentText),
              $"Supprimer {personality.FirstName} {personality.LastName} ?" }
        };
        var dialog = await DialogService.ShowAsync<ConfirmDeleteDialog>("Confirmer la suppression",
            parameters, new DialogOptions { MaxWidth = MaxWidth.Small });
        var result = await dialog.Result;

        if (!result!.Canceled)
        {
            await PersonalityClient.DeletePersonalityAsync(personality.Id);
            Snackbar.Add("Personnalité supprimée.", Severity.Success);
            await _dataGrid!.ReloadServerData();
        }
    }
}
```

## Form Dialog Pattern

```razor
<MudDialog>
    <DialogContent>
        <MudForm @ref="_form" @bind-IsValid="_isValid">
            <MudGrid>
                <MudItem xs="6">
                    <MudTextField @bind-Value="_firstName" Label="Prénom" Required />
                </MudItem>
                <MudItem xs="6">
                    <MudTextField @bind-Value="_lastName" Label="Nom" Required />
                </MudItem>
                <MudItem xs="6">
                    <MudSelect @bind-Value="_category" Label="Catégorie" Required>
                        @foreach (var cat in _categories)
                        {
                            <MudSelectItem Value="@cat">@cat</MudSelectItem>
                        }
                    </MudSelect>
                </MudItem>
                <MudItem xs="6">
                    <MudTextField @bind-Value="_nationality" Label="Nationalité" Required />
                </MudItem>
                <MudItem xs="6">
                    <MudDatePicker @bind-Date="_birthDate" Label="Date de naissance" Required />
                </MudItem>
                <MudItem xs="6">
                    <MudDatePicker @bind-Date="_deathDate" Label="Date de décès" Clearable />
                </MudItem>
                <MudItem xs="12">
                    <MudTextField @bind-Value="_bio" Label="Biographie" Lines="4" Required />
                </MudItem>
                <MudItem xs="12">
                    <MudTextField @bind-Value="_imageUrl" Label="URL de l'image" />
                </MudItem>
            </MudGrid>
        </MudForm>
    </DialogContent>
    <DialogActions>
        <MudButton OnClick="Cancel">Annuler</MudButton>
        <MudButton Color="Color.Primary" Disabled="!_isValid" OnClick="Submit">
            @(_isEditMode ? "Modifier" : "Créer")
        </MudButton>
    </DialogActions>
</MudDialog>
```

## Delete Confirmation Dialog

```razor
<MudDialog>
    <DialogContent>
        <MudText>@ContentText</MudText>
    </DialogContent>
    <DialogActions>
        <MudButton OnClick="Cancel">Annuler</MudButton>
        <MudButton Color="Color.Error" Variant="Variant.Filled" OnClick="Confirm">
            Supprimer
        </MudButton>
    </DialogActions>
</MudDialog>

@code {
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;
    [Parameter] public string ContentText { get; set; } = "";

    private void Cancel() => MudDialog.Cancel();
    private void Confirm() => MudDialog.Close(DialogResult.Ok(true));
}
```

## Key MudBlazor Components Used

| Component | Usage |
|-----------|-------|
| `MudDataGrid` | Server-side paginated data table |
| `MudDialog` | Modal dialogs for forms and confirmation |
| `MudForm` | Form validation |
| `MudTextField` | Text input with debounce search |
| `MudSelect` | Category filter dropdown |
| `MudDatePicker` | Date selection |
| `MudSnackbar` | Success/error notifications |
| `MudButton` / `MudIconButton` | Actions |
| `MudPaper` | Card-like containers |
