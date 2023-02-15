<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptAllLetter.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptAllLetter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <style>
        .badgechk label {
            margin: 0 0 0 5px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
        }
        


       <%-- function Search_Gridview2(strKey) {
            try {


              //  document.getElementById("<%=this.ddlpagesize.ClientID %>").value = 1000;

               // $('#<%= ddlpagesize.ClientID %>').trigger('change');


                var strData = strKey.value.toLowerCase().split(" ");
                /*alert()*/
                var tblData = document.getElementById("<%=this.gvEmpList.ClientID %>");

                var rowData;
                for (var i = 1; i < tblData.rows.length; i++) {
                    rowData = tblData.rows[i].innerHTML;
                    var styleDisplay = 'none';
                    for (var j = 0; j < strData.length; j++) {
                        if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                            styleDisplay = '';
                        else {
                            styleDisplay = 'none';
                            break;
                        }
                    }
                    tblData.rows[i].style.display = styleDisplay;
                }
            }

            catch (e) {
                alert(e.message);
            }
        }--%>
        function GetEmployeeform() {
            $('#EmployeeEntry').modal('toggle');
        }
        function CloseModal() {
            $('#EmployeeEntry').modal('hide');
        }
    </script>
    <style type="text/css">
        .chzn-single {
            border-radius: 3px!important;
            height: 29px!important;
        }
    </style>
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

  
       
            <div class="card card-fluid mb-1 mt-2">
                <div class="card-body">
                 
                    <div class="row">

                              <div class="col-md-2 mt-2">
                            <div class="from-group">
                                <asp:Label ID="prjName" CssClass="form-label" runat="server">Project Name </asp:Label>
                                <asp:DropDownList ID="ddlPrjName" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" ></asp:DropDownList>
                            </div>
                        </div>
                        
                     <%--  <div class="col-md-2 ml-2 mt-2">
                           <div class="form-group">
                            
                                <asp:TextBox ID="txtSrcCustomer" runat="server" CssClass="form-control form-control-sm" TabIndex="14" visible="false"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnFindCustomer" runat="server" CssClass="" OnClick="imgbtnFindCustomer_Click" ><span style="font-size:14px;">Customer Name&nbsp;<i class="fas fa-search"></i></span></asp:LinkButton>
                                <asp:DropDownList ID="ddlCustName" runat="server" CssClass="form-control form-control-sm chzn-select"  AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                           </div>--%>
                         <div class="col-md-1" style="margin-top: 27px;">
                            
                     <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                           
                        </div>
                        </div>
                    </div>
                

                    <div class="card-body">


                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-lg-12">
                                <asp:GridView ID="gvcustList" runat="server" CssClass="table-striped  table-bordered grvContentarea"
                                    AutoGenerateColumns="False" AllowPaging="false" 
                                    ShowFooter="True" PageSize="15">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server"  Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="16px"/>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Customer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcustname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="16px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblunitdesc" runat="server"
                                                    Text='<%#Convert.ToString(Convert.ToString(DataBinder.Eval(Container.DataItem, "unitdesc")).Trim())  %>'
                                                    Width="250px"> 
                                              
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="16px" />
                                        </asp:TemplateField>

                         
                                         <asp:TemplateField HeaderText="Operation">
                                            <ItemTemplate>
                                                 <div class="dropdown">
                                                    <button type="button" class="btn btn-default btn-xs dropdown-toggle" data-toggle="dropdown">
                                                        Action
                                                        
                                                    </button>
                                                    <ul class="dropdown-menu">
                                                       

                                                           <li class="mt-2">
                                                            <asp:HyperLink  runat="server"  ID="lnkOfferLetter" Target="_blank"  CssClass="dropdown-item" ToolTip="Congratulation Letter"
                                                                NavigateUrl='<%# "~/LetterDefault?Type=10003 &Page=CustLetter &Entry=Congratulation Letter &custid="+Eval("usircode")+" &pactcode="+Eval("pactcode") %>'>
                                                                 Congartulation Letter</asp:HyperLink>
                                                        </li>
                                                        
                                                           <li class="mt-2">
                                                            <asp:HyperLink  runat="server"  ID="HyperLink1" Target="_blank"  CssClass="dropdown-item" ToolTip="Registration Letter"
                                                                NavigateUrl='<%# "~/LetterDefault?Type=10004 &Page=CustLetter &Entry=Registration Letter &custid="+Eval("usircode")+" &pactcode="+Eval("pactcode") %>'>
                                                                 Registration Letter</asp:HyperLink>
                                                        </li>
                                                        
                                                           <li class="mt-2">
                                                            <asp:HyperLink  runat="server"  ID="HyperLink2" Target="_blank"  CssClass="dropdown-item" ToolTip="Reminder Letter"
                                                                NavigateUrl='<%# "~/LetterDefault?Type=10005 &Page=CustLetter &Entry=Reminder Letter &custid="+Eval("usircode")+" &pactcode="+Eval("pactcode") %>'>
                                                                 Reminder Letter</asp:HyperLink>
                                                        </li>



                                                    </ul>
                                                </div>
                                              
                                               

                                               <%-- <asp:HyperLink runat="server" ID="lnkAppoint" Target="_blank" ToolTip="Appointment Letter"
                                                                NavigateUrl='<%# "~/LetterDefault?Type=10002 &Page=NewRec &Entry=appoinment Letter &advno="+Eval("advno") %>'
                                                                 CssClass="text-primary"> <i class="fa fa-envelope"></i></asp:HyperLink>

                                                 <asp:HyperLink  runat="server" ID="lnkConfirmation" Target="_blank"
                                                                           NavigateUrl='<%# "~/LetterDefault?Type=10025 &Page=NewRec &Entry=confirmation Letter &advno="+Eval("advno") %>'
                                                                           CssClass="btn btn-success btn-sm">Confirmation Letter</asp:HyperLink>--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                     
                                    </Columns>
                                   <FooterStyle CssClass="grvFooterNew" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                      <RowStyle CssClass="grvRowsNew" />
                        <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                            </div>
                        </div>

                    </div>

                </div>
       
     
</asp:Content>
