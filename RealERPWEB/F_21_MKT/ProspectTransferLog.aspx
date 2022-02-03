<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ProspectTransferLog.aspx.cs" Inherits="RealERPWEB.F_21_MKT.ProspectTransferLog" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 

   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="card mt-5">
      <div class="card-header">
   <div class="row">
      <div class="col-md-12">
         <div class="col-md-12 form-inline">





              <label class="">From : </label>
               <asp:TextBox ID="txtFdate" runat="server" autocomplete="off" CssClass="form-control ml-2"></asp:TextBox>
               <cc1:CalendarExtender ID="txtFdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtFdate"></cc1:CalendarExtender>
        
               <label class="ml-2">To : </label>
               <asp:TextBox ID="txtTdate" runat="server" autocomplete="off" CssClass="form-control ml-2"></asp:TextBox>
               <cc1:CalendarExtender ID="txtTdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtTdate"></cc1:CalendarExtender>
               <asp:LinkButton ID="lnkbtnOK" runat="server" OnClick="lnkbtnOK_Click" CssClass="btn btn-primary  ml-2">Show</asp:LinkButton>
       
         </div>
      </div>
   </div>
</div>
                <div class="card-body">
                    <div class="row">
                    <div class="col-md-12">

                        <div class="">
                            <asp:GridView ID="gvtransLog" runat="server" AutoGenerateColumns="false"
                                CssClass="table table-bordered table-striped display" AllowPaging="True" ViewStateMode="Enabled"
                                AllowSorting="True" PageSize="500">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                               Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Prospect Name" SortExpression="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="From Associate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfrom" runat="server"  Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "from_assign")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="To Associate" SortExpression="category">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldFrom" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "to_assign")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transfer date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMsg" runat="server"  Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "transfer_date","{0:dd/MMM/yyyy}")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transfer By">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldtime" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "transfer_by")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="gvPagination" />
                                <EmptyDataTemplate>
                                    <div style="color: red; text-align: center !important; font-style: italic; font-size: 15px;">No records to display.</div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                    </div>
                </div>



            </div>
             <script>

                 $(document).ready(function () {
                     $('#ContentPlaceHolder1_gvNotificaitons').DataTable();
                 });

             </script>
        </ContentTemplate>

       
    </asp:UpdatePanel>
</asp:Content>