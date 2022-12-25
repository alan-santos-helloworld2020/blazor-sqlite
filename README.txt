1) crie o modelo
    public class Contato
    {

        
        public int id { get; set; } = 0;
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo telefone é obrigatório")]
        public string telefone { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo email é obrigatório")]
        public string email { get; set; } = string.Empty;

    }

****************************************************************************************
2) crie o context

    public class UsuarioDbContext:DbContext
    {
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> options):base(options)
        {


        }
        public DbSet<Contato> contatos {get;set;}
        
    }

****************************************************************************************
3) configure o arquivo Program.cs

    builder.Services.AddScoped<UsuarioService>();
    builder.Services.AddDbContext<UsuarioDbContext>(optionsAction =>{
        optionsAction.UseSqlite(builder.Configuration.GetConnectionString("conexao"));
    });

****************************************************************************************
4) crie o serviço

     public class UsuarioService
    {

        private UsuarioDbContext ctx;

        public UsuarioService(UsuarioDbContext _ctx){
            this.ctx = _ctx;
        }

        public async Task<List<Contato>> findAll(){
            return await this.ctx.contatos.ToListAsync();
        }

        public async Task<Contato> findById(int id){
            var ct  = await this.ctx.contatos.FirstOrDefaultAsync<Contato>(c => c.id == id);
            if(ct != null){

                return ct;

            }else{

                return null;
            }
        }


       public async  Task<List<Contato>> save(Contato contato){
            await this.ctx.contatos.AddAsync(contato);
            await this.ctx.SaveChangesAsync();
            return await this.ctx.contatos.ToListAsync();

        }
    }

****************************************************************************************
5) injete na pagina razor

@page "/Usuarios"
@inject UsuarioService service;
@using System.Text;

<div class="col-lg-8 mx-auto mt-5">
 <p class="text-center fw-bolder h5">Tabela de Contatos</p>     
<table class="table mt-5">
    <thead class="bg-secondary text-center text-white">
          <tr>
            <th>Id</th>
            <th>Nome</th>
            <th>Telefone</th>
            <th>Email</th>
          </tr>
    </thead>
    <tbody>
      @foreach(var ct in contatos){
          <tr class="text-center">
            <td>@ct.id</td>
            <td>@ct.nome</td>
            <td>@ct.telefone</td>
            <td>@ct.email</td>                 
          </tr>
      }
    </tbody>
</table>
</div>
@code {

    public List<Contato> contatos = new List<Contato>();

    protected override async Task OnInitializedAsync(){
      //JS.InvokeVoidAsync("alert","hello");
      this.contatos = await Task.Run(() => service.findAll());
    }
     
}



