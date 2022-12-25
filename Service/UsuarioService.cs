using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.context;
using BlazorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Service
{
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

                return new Contato();
            }
        }

       public async  Task<List<Contato>> save(Contato contato){
            await this.ctx.contatos.AddAsync(contato);
            await this.ctx.SaveChangesAsync();
            return await this.ctx.contatos.ToListAsync();

        }

        public async  Task<List<Contato>> editar(Contato contato){
            var edit = await this.ctx.contatos.FirstOrDefaultAsync<Contato>(c => c.id == contato.id);
            if(edit != null){
                edit.id = contato.id;
                edit.telefone = contato.telefone;
                edit.email = contato.email;
                this.ctx.Entry(edit).State = EntityState.Modified;
                await this.ctx.SaveChangesAsync();
            }
            return await this.ctx.contatos.ToListAsync();

        }

        public async  Task<List<Contato>> delete(int id){
            var ct = await this.ctx.contatos.FindAsync(id);
            if(ct != null){
            this.ctx.Remove<Contato>(ct);    
            await this.ctx.SaveChangesAsync();
            return await this.ctx.contatos.ToListAsync();
            }
            return await this.ctx.contatos.ToListAsync();
        }
    }
}